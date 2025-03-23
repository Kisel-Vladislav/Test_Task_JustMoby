using Nuclear.Utilities;
using System.Collections.Generic;

namespace _Project.Infrastructure.Services.GameFactory
{
    public interface IGameFactory
    {
        float CubeSize { get; }

        void WarmUp();

        IEnumerable<DragItem> CreateBuildCubes();
        DragItem CreateTowerCube(string id);
    }
}