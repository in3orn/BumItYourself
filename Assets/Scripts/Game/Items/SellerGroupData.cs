using System;

namespace Krk.Bum.Game.Items
{
    [Serializable]
    public class SellerGroupData
    {
        public string Name;

        public string[] CollectionsIds;
        public string[] BodiesIds;
        public string[] BagsIds;
        public string[] SticksIds;
        public string[] GlassesIds;
    }
}
