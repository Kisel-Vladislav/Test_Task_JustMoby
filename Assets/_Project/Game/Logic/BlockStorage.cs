using _Project.Game.Logic.Message;
using Nuclear.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Game.Logic
{
    public class BlockStorage : MonoBehaviour
    {
        [SerializeField] private Transform ContentParent;
        [SerializeField] private RectTransform GameUI;

        private MessageDisplay _messageDisplay;

        [Inject]
        public void Construct(MessageDisplay messageDisplay)
        {
            _messageDisplay = messageDisplay;
        }

        public void Fill(IEnumerable<DragItem> blocks)
        {
            foreach (var block in blocks)
            {
                block.transform.SetParent(ContentParent, false);

                block.DraggedItemParent = GameUI;
                block.OnEndDragEvent.AddListener(Drop);
            }
        }

        private void Drop(DragItem arg0, DropZone zone)
        {
            if (zone == null)
                _messageDisplay.Send(MessageKey.BlockDisappeared);
        }
    }
}