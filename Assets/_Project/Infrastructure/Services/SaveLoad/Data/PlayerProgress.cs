using System;
using System.Collections.Generic;

namespace _Project.Infrastructure.Services.SaveLoad
{
    [Serializable]
    public class PlayerProgress
    {
        public TowerProgress Tower;

        public PlayerProgress()
        {
            Tower = new()
            {
                Blocks = new List<BlockSaveData>()
            };
        }
    }
}