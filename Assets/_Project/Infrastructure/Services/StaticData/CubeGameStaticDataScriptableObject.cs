using UnityEngine;

namespace _Project.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "CubeGameStaticData", menuName = "StaticData/CubeGame")]
    public class CubeGameStaticDataScriptableObject : ScriptableObject
    {
        public CubeGameStaticData Data;
    }
}