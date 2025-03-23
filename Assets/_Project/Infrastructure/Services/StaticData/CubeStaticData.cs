using System;
using UnityEngine;

namespace _Project.Infrastructure.Services.StaticData
{
    [Serializable]
    public class CubeStaticData
    {
        public string ID = Guid.NewGuid().ToString();
        public Color Color;
    }
}