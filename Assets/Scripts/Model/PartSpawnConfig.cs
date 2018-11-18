using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Part Spawn")]
    public class PartSpawnConfig : ScriptableObject
    {
        public string Id;

        public int MaxSpawnWeight;
    }
}
