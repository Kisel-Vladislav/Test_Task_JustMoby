using _Project.Game.Logic;
using _Project.Infrastructure.Services.GameFactory;
using _Project.Infrastructure.Services.SaveLoad;
using _Project.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.Infrastructure
{
    public class LevelBootstrapper : IInitializable
    {
        private readonly BlockStorage _blockStorage;
        private readonly Tower _tower;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;

        public LevelBootstrapper(BlockStorage blockStorage, Tower tower, IStaticDataService staticDataService, IGameFactory gameFactory, ISaveLoadService saveLoadService)
        {
            _blockStorage = blockStorage;
            _tower = tower;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            SetupBlockStorage();
            RegisterProgressListener();
            LoadProgress();
        }

        private void RegisterProgressListener() =>
            _saveLoadService.Register(_tower);

        private void LoadProgress() =>
            _saveLoadService.Load();

        private void SetupBlockStorage()
        {
            var gameData = _staticDataService.GetGameData();
            _blockStorage.Fill(_gameFactory.CreateBuildCubes());
        }
    }
}