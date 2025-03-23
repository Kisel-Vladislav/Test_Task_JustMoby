using _Project.Infrastructure.Services.GameFactory;
using _Project.Infrastructure.Services.Localization;
using _Project.Infrastructure.Services.SaveLoad;
using _Project.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalizationService();
            BindStaticDataService();
            BindSaveLoadService();
            BindGameFactory();

            Container.Bind<IInitializable>().To<GameBootstrapper>().AsSingle();
        }

        private void BindLocalizationService() =>
            Container.Bind<ILocalizationService>().To<MockLocalizationService>().AsSingle();

        private void BindSaveLoadService() =>
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();

        private void BindStaticDataService() =>
            Container.Bind<IStaticDataService>().To<SOStaticDataService>().AsSingle();

        private void BindGameFactory() =>
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
}