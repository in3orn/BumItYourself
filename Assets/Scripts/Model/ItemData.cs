using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class ItemData
    {
        public string Id;
        public string Name;

        public int Count;
        public int Reward;

        public ImageData Image;

        public ItemPartData[] RequiredParts;
    }
}
