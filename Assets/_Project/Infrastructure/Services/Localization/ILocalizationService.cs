namespace _Project.Infrastructure.Services.Localization
{
    public interface ILocalizationService
    {
        void SetLanguage(string languageCode);
        string GetLocalizedString(string key);
    }
}