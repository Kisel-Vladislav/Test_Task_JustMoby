using _Project.Infrastructure.Services.GameFactory;
using _Project.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;

        public GameBootstrapper(IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
        }

        public void Initialize()
        {
            LoadStaticData();

            _gameFactory.WarmUp();
        }

        private void LoadStaticData() =>
            _staticDataService.LoadBuildBlock();
    }
}