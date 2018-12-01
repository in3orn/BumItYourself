using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class ModelData
    {
        public CollectionData[] Collections;

        public PartData[] Parts;

        public int Cash;
    }
}
