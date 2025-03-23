using _Project.Infrastructure.Services.StaticData;

namespace _Project.Game.Logic.GameRule.Placement
{
    public interface IPlacementRule
    {
        bool CanPlace(CubeStaticData newBlock, Tower tower);
    }
}