using System;
using System.Collections.Generic;

namespace _Project.Infrastructure.Services.SaveLoad
{
    [Serializable]
    public class TowerProgress
    {
        public List<BlockSaveData> Blocks;
    }
}