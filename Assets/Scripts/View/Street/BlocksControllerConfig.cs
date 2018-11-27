using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    [CreateAssetMenu(menuName = "Krk/View/Street/Block Controller")]
    public class BlocksControllerConfig : ScriptableObject
    {
        public float SpawnDistance;

        public FloatRange SpawnIntervalRange;
        public FloatRange SpawnYRange;
        
        public bool InheritSpawnZFromY;
        public float SpawnZ;
    }
}
