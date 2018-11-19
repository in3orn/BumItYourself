using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class RequiredPartData
    {
        public string Id;
        public string Name;

        public ImageData Image;

        public int Count;
        public int RequiredCount;
    }
}
