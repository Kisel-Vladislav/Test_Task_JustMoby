using _Project.Game.Logic;
using _Project.Game.Logic.Message;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private MessageDisplay _messageDisplay;
        [SerializeField] private BlockStorage _blockStorage;
        [SerializeField] private Tower _tower;
        public override void InstallBindings()
        {
            Container.Bind<MessageDisplay>().FromInstance(_messageDisplay).AsSingle();
            Container.Bind<BlockStorage>().FromInstance(_blockStorage).AsSingle();
            Container.Bind<Tower>().FromInstance(_tower).AsSingle();

            Container.Bind<IInitializable>().To<LevelBootstrapper>().AsSingle();
        }
    }
}