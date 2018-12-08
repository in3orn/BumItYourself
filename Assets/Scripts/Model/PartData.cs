using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class PartData
    {
        public string Id;
        public string Name;

        public ImageData Image;

        public int Count;

        public bool IsCollection;
    }
}
