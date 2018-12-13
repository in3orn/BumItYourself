using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Collection")]
    public class CollectionConfig : ScriptableObject
    {
        public string Id;
        public string Name;

        public ImageData Image;

        public int Price;

        public ItemConfig[] Items;
    }
}
