using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Item")]
    public class ItemConfig : ScriptableObject
    {
        public string Id;
        public string Name;

        public int Reward;

        public ImageData Image;

        public ItemPartData[] RequiredParts;
    }
}
