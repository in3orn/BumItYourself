using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Model")]
    public class ModelConfig : ScriptableObject
    {
        public CollectionConfig[] Collections;

        public PartConfig[] Parts;
    }
}
