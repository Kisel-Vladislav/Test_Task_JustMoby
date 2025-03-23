using _Project.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Project.Game.Logic
{
    public class WhenDestroySaveProgress : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        private void OnDestroy() =>
            _saveLoadService.Save();
    }
}