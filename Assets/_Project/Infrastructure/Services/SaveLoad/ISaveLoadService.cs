namespace _Project.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void Load();
        void Register(IProgressListener progressManager);
        void Save();
    }
}