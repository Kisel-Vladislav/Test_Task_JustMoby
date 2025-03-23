namespace _Project.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        CubeStaticData GetCube(string id);
        CubeGameStaticData GetGameData();
        void LoadBuildBlock();
    }
}