using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class PlayerLookData
    {
        public string CurrentBodyId;
        public string CurrentBagId;
        public string CurrentStickId;
        public string CurrentGlassesId;
        public string CurrentBeardId;

        public PlayerItemData[] Bodies;
        public PlayerItemData[] Bags;
        public PlayerItemData[] Sticks;
        public PlayerItemData[] Glasses;
        public PlayerItemData[] Beards;
    }
}
