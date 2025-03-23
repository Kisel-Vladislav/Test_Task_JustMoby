using _Project.Game.Logic;
using _Project.Infrastructure.Services.StaticData;
using Nuclear.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;

        private CubeGameStaticData _cubeGameStaticData;
        private Dictionary<string, CubeStaticData> dictionary;

        private GameObject CubePrefab => _cubeGameStaticData.Prefab;

        public float CubeSize => _cubeGameStaticData.CubeSize;

        public GameFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void WarmUp()
        {
            _cubeGameStaticData = _staticData.GetGameData();
            dictionary = _cubeGameStaticData.CubeStaticDates.ToDictionary(x => x.ID, x => x);
        }

        public IEnumerable<DragItem> CreateBuildCubes()
        {
            foreach (var cubeStaticData in _cubeGameStaticData.CubeStaticDates)
            {
                var gameObject = Object.Instantiate(CubePrefab);

                gameObject.GetComponent<Image>().color = cubeStaticData.Color;

                var buildBlock = gameObject.AddComponent<BuildBlock>();
                buildBlock.Id = cubeStaticData.ID;

                var dragItem = gameObject.GetComponent<DragItem>();
                yield return dragItem;
            }
        }

        public DragItem CreateTowerCube(string id)
        {
            var gameObject = Object.Instantiate(CubePrefab);

            gameObject.GetComponent<Image>().color = dictionary[id].Color;
            gameObject.AddComponent<TowerBlock>();

            var dragItem = gameObject.GetComponent<DragItem>();
            return dragItem;
        }
    }
}