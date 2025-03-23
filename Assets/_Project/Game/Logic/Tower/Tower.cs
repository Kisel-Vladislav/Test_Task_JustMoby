using _Project.Game.Logic.GameRule.Placement;
using _Project.Game.Logic.Message;
using _Project.Infrastructure;
using _Project.Infrastructure.Services.GameFactory;
using _Project.Infrastructure.Services.SaveLoad;
using _Project.Infrastructure.Services.StaticData;
using DG.Tweening;
using Nuclear.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Game.Logic
{
    public class Tower : MonoBehaviour, IProgressListener
    {
        [SerializeField] private RectTransform GameUI;
        [SerializeField] private DropZone DropZone;
        [SerializeField] private RectTransform TowerRoot;

        private MessageDisplay _messageDisplay;
        private IStaticDataService _staticDataService;
        private IGameFactory _gameFactory;

        private List<IPlacementRule> gamePlacementRuleRules;
        private List<DragItem> _dragItems = new();
        private Dictionary<DragItem, string> _dragItemsSave = new();

        private Vector2 lastPosition;
        private RectTransform DropZoneRectTransform;

        private float CubeSize => _gameFactory.CubeSize;
        private int CubeCount => _dragItems.Count;
        private float HalfCubeSize => CubeSize * 0.5f;

        public List<CubeStaticData> CubeData;

        [Inject]
        public void Construct(MessageDisplay messageDisplay, IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            _messageDisplay = messageDisplay;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
        }

        private void Awake() =>
            SetupDropZone();

        private void Start()
        {
            DropZone.OnDraggedItemDrop.AddListener(Drop);

            gamePlacementRuleRules = _staticDataService.GetGameData().GamePlacementRules;
        }

        private void OnDestroy() =>
            DropZone.OnDraggedItemDrop.RemoveListener(Drop);

        private void SetupDropZone()
        {
            DropZoneRectTransform = DropZone.GetComponent<RectTransform>();

            DropZoneRectTransform.AnchorToParent().ResetOffsets();
        }
        private void RecalculateDropZone(Vector2 lastBlockPosition)
        {
            if (CubeCount == 1)
                DropZoneRectTransform.AnchorToVerticalCenter().ResetOffsets();

            DropZoneRectTransform.anchoredPosition = new Vector2(lastBlockPosition.x, DropZoneRectTransform.anchoredPosition.y);
            DropZoneRectTransform.SetWidth(CubeSize);
            DropZoneRectTransform.Bottom(CubeSize * CubeCount);
        }
        private void UpdateDropZoneState()
        {
            if (CubeCount == 0)
            {
                lastPosition = Vector2.zero;
                SetupDropZone();
                return;
            }

            lastPosition = ((RectTransform)_dragItems.Last().transform).anchoredPosition;
            RecalculateDropZone(lastPosition);
        }

        private void Drop(DropZone zone, TemporaryDragItem droppedBlock)
        {
            if (!IsValidDrop(droppedBlock))
                return;

            var dropPosition = GetDropPosition(droppedBlock.transform.position);
            var id = GetId(droppedBlock);

            if (!IsPlacementAllowedByRules(id))
            {
                ShowMessage(MessageKey.BlockDisappeared);
                return;
            }

            PositionNewBlock(dropPosition, id);
            _messageDisplay.Send(MessageKey.BlockPlaced);

            if (IsHeightLimitExceeded())
                ShowMessage(MessageKey.HeightLimitReached);
        }

        private string GetId(TemporaryDragItem droppedBlock) =>
            droppedBlock.DragItem.GetComponent<BuildBlock>().Id;

        private Vector2 GetDropPosition(Vector3 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            TowerRoot,
                            position,
                            null,
                            out var dropPosition
                    );
            return dropPosition;
        }

        private DragItem PositionNewBlock(Vector2 dropPosition, string id, bool useExactPosition = false)
        {
            var newBlock = SetupTowerBlock(id);
            var blockTransform = (RectTransform)newBlock.gameObject.transform;
            blockTransform.AnchorToBottomCenter();

            var targetPosition = useExactPosition ? dropPosition : GetAdjustedBlockPosition(dropPosition);

            blockTransform.anchoredPosition = new Vector2(targetPosition.x, targetPosition.y + 50);
            blockTransform.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.OutQuad);

            lastPosition = targetPosition;
            _dragItems.Add(newBlock);
            _dragItemsSave.Add(newBlock, id);
            CubeData.Add(_staticDataService.GetCube(id));

            RecalculateDropZone(lastPosition);

            return newBlock;
        }

        private Vector2 GetAdjustedBlockPosition(Vector2 dropPosition)
        {
            if (CubeCount == 0)
            {
                dropPosition.y = HalfCubeSize;
                dropPosition.x = ClampXToTowerBounds(dropPosition.x);
                return dropPosition;
            }
            else
            {
                var xOffset = Random.Range(-HalfCubeSize, HalfCubeSize);
                var newX = ClampXToTowerBounds(lastPosition.x - xOffset);
                return new Vector2(newX, HalfCubeSize + CubeCount * CubeSize);
            }
        }

        private DragItem SetupTowerBlock(string id)
        {
            var newBlock = _gameFactory.CreateTowerCube(id);
            newBlock.gameObject.transform.SetParent(TowerRoot, false);
            newBlock.DraggedItemParent = GameUI;

            newBlock.OnEndDragEvent.AddListener(RemoveAndShiftBlock);
            return newBlock;
        }

        private float ClampXToTowerBounds(float x)
        {
            var halfWidth = TowerRoot.rect.width * 0.5f - HalfCubeSize;
            return Mathf.Clamp(x, -halfWidth, halfWidth);
        }

        private void RemoveAndShiftBlock(DragItem draggedItem, DropZone dropZone)
        {
            if (!IsDumpDropZone(dropZone))
                return;

            var removedIndex = RemoveBlock(draggedItem, dropZone);

            ShiftBlocksDown(removedIndex);
            UpdateDropZoneState();

            _messageDisplay.Ping(MessageKey.BlockRemoved);
        }

        private int RemoveBlock(DragItem draggedItem, DropZone dropZone)
        {
            var index = _dragItems.IndexOf(draggedItem);
            _dragItems.Remove(draggedItem);
            _dragItemsSave.Remove(draggedItem);
            CubeData.RemoveAt(index);

            DestroyBlock(draggedItem.gameObject, dropZone.gameObject);
            return index;
        }

        private void ShiftBlocksDown(int removedIndex)
        {
            if (removedIndex >= CubeCount)
                return;

            foreach (var item in _dragItems.Skip(removedIndex))
            {
                var rectTransform = (RectTransform)item.transform;
                lastPosition = rectTransform.anchoredPosition;

                var targetPosition = new Vector2(lastPosition.x, lastPosition.y - CubeSize);
                rectTransform.DOAnchorPos(targetPosition, 1f).SetEase(Ease.OutBounce);
            }
        }

        private void DestroyBlock(GameObject block, GameObject target)
        {
            var rectTransform = block.GetComponent<RectTransform>();
            rectTransform.AnchorToMiddleCenterWithoutMoving();

            var rectTagret = (RectTransform)target.transform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    TowerRoot,
                    rectTagret.position,
                    null,
                    out var dropPosition
            );

            var sequence = DOTween.Sequence();

            sequence.Append(rectTransform.DOAnchorPos(dropPosition, 0.5f)
                .SetEase(Ease.InOutQuad));

            sequence.Append(rectTagret.DOShakeScale(1f)
                .SetEase(Ease.OutBounce));

            sequence.Join(rectTransform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack));

            sequence.Join(rectTransform.DORotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360)
                .SetEase(Ease.InBack));

            sequence.OnKill(() => Destroy(block.gameObject));

            sequence.Play();
        }

        private bool IsValidDrop(TemporaryDragItem droppedBlock)
        {
            if (!IsBuildBlock(droppedBlock))
            {
                ShowMessage(MessageKey.BlockDisappeared);
                return false;
            }

            if (IsHeightLimitExceeded())
            {
                ShowMessage(MessageKey.HeightLimitReached);
                return false;
            }

            return true;
        }

        private bool IsPlacementAllowedByRules(string id)
        {
            foreach (var placementRule in gamePlacementRuleRules)
            {
                var newBlockStaticData = _staticDataService.GetCube(id);

                if (!placementRule.CanPlace(newBlockStaticData, this))
                    return false;
            }

            return true;
        }

        private bool IsHeightLimitExceeded() =>
            TowerRoot.rect.height < DropZoneRectTransform.GetBottom();

        private BuildBlock IsBuildBlock(TemporaryDragItem droppedBlock) =>
            droppedBlock.gameObject.GetComponent<BuildBlock>();

        private bool IsDumpDropZone(DropZone dropZone) =>
            dropZone != null && dropZone.GetComponent<Dump>() != null;

        private void ShowMessage(string messageKey) =>
            _messageDisplay.Ping(messageKey);

        public void SaveProgress(PlayerProgress progress)
        {
            var towerProgress = new TowerProgress();
            towerProgress.Blocks = new List<BlockSaveData>();

            foreach (var item in _dragItemsSave)
            {
                var position = item.Key.GetComponent<RectTransform>().anchoredPosition;
                var id = item.Value;

                BlockSaveData blockSave = new()
                {
                    Position = position,
                    Id = id
                };

                towerProgress.Blocks.Add(blockSave);
            }
            progress.Tower = towerProgress;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            var sortedBlocks = progress.Tower.Blocks.OrderBy(block => block.Position.y);

            foreach (var blockSave in sortedBlocks)
                PositionNewBlock(blockSave.Position, blockSave.Id, useExactPosition: true);
        }
    }
}