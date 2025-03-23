using System;
using UnityEngine;

namespace _Project.Infrastructure.Services.SaveLoad
{
    [Serializable]
    public class BlockSaveData
    {
        public Vector2 Position;
        public string Id;
    }
}