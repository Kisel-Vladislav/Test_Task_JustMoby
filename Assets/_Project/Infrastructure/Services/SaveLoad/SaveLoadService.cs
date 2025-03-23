using System.Collections.Generic;
using UnityEngine;

namespace _Project.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "ProgressKey";

        private List<IProgressListener> progressManagers = new();
        private PlayerProgress progress;

        public void Register(IProgressListener progressManager) =>
            progressManagers.Add(progressManager);

        public void Save()
        {
            foreach (var progressManager in progressManagers)
                progressManager.SaveProgress(progress);

            var json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(ProgressKey, json);
        }

        public void Load()
        {
            var json = PlayerPrefs.GetString(ProgressKey);

            progress = new PlayerProgress();

            if (string.IsNullOrEmpty(json))
                return;

            progress = JsonUtility.FromJson<PlayerProgress>(json);

            foreach (var progressManager in progressManagers)
                progressManager.LoadProgress(progress);
        }
    }
}