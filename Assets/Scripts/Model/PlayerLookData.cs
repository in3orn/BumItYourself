using System;

namespace Krk.Bum.Model
{
    [Serializable]
    public class PlayerLookData
    {
        public string CurrentBodyId;
        public string CurrentBagId;

        public PlayerItemData[] Bodies;
        public PlayerItemData[] Bags;
    }
}
