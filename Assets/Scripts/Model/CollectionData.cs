using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class CollectionData
    {
        private const string KeyFormat = "{0}-{1}";
        private const string UnlockedKey = "unlocked";

        public string Id;
        public string Name;

        public ImageData Image;

        public int Price;

        public bool Unlocked;

        public ItemData[] Items;
    }
}
