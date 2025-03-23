namespace _Project.Infrastructure.Services.SaveLoad
{
    public interface IProgressListener
    {
        void SaveProgress(PlayerProgress progress);
        void LoadProgress(PlayerProgress progress);

    }
}