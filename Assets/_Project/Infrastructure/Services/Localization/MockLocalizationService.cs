using _Project.Game.Logic.Message;
using System;
using System.Collections.Generic;

namespace _Project.Infrastructure.Services.Localization
{
    public class MockLocalizationService : ILocalizationService
    {
        private readonly Dictionary<string, string> _localizedStrings = new()
        {
            { MessageKey.BlockPlaced, "Кубик установлен!" },
            { MessageKey.BlockRemoved, "Кубик удалён!" },
            { MessageKey.BlockDisappeared, "Кубик исчез!" },
            { MessageKey.HeightLimitReached, "Превышено ограничение по высоте!" }
        };

        public void SetLanguage(string languageCode)
        {
            throw new NotImplementedException();
        }

        public string GetLocalizedString(string key)
        {
            if (_localizedStrings.TryGetValue(key, out var value))
                return value;

            throw new InvalidOperationException($"Localization key '{key}' not found.");
        }
    }
}