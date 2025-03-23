using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Infrastructure.Services.StaticData
{
    public class SOStaticDataService : IStaticDataService
    {
        private const string GameStaticDataPath = "StaticData/CubeGameStaticData";

        private CubeGameStaticData _gameStaticData;
        private Dictionary<string, CubeStaticData> _cubeStaticDatas;

        public CubeStaticData GetCube(string id) =>
            _cubeStaticDatas[id];

        public CubeGameStaticData GetGameData() =>
            _gameStaticData;

        public void LoadBuildBlock()
        {
            var gameDataWrapper = Resources.Load<CubeGameStaticDataScriptableObject>(GameStaticDataPath);
            _gameStaticData = gameDataWrapper.Data;

            _cubeStaticDatas = _gameStaticData.CubeStaticDates.ToDictionary(x => x.ID, x => x);
        }
    }
}