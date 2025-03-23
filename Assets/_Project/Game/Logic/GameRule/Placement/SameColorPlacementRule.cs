using _Project.Infrastructure.Services.StaticData;
using ModestTree;
using System.Linq;

namespace _Project.Game.Logic.GameRule.Placement
{
    public class SameColorPlacementRule : IPlacementRule
    {
        public bool CanPlace(CubeStaticData newBlock, Tower tower) =>
            tower.CubeData.IsEmpty() || tower.CubeData.Last().Color == newBlock.Color;
    }
}