using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class PlayerItemData
    {
        public string Id;
        public string Name;

        public float Price;

        public ImageData Image;

        public bool Unlocked;
    }
}
