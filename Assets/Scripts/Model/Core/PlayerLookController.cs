using Krk.Bum.Model.Utils;
using System;
using UnityEngine.Events;

namespace Krk.Bum.Model.Core
{
    public class PlayerLookController
    {
        public UnityAction<PlayerItemData> OnBodyChanged;


        private readonly PlayerLookData playerLookData;

        private readonly PlayerLookLoader playerLookLoader;

        private readonly PlayerItemLoader playerItemLoader;


        public string CurrentBodyId { get { return playerLookData.CurrentBodyId; } }

        public PlayerItemData[] Bodies {  get { return playerLookData.Bodies; } }


        public PlayerLookController(PlayerLookData playerLookData, 
            PlayerLookLoader playerLookLoader,
            PlayerItemLoader playerItemLoader)
        {
            this.playerLookData = playerLookData;
            this.playerLookLoader = playerLookLoader;
            this.playerItemLoader = playerItemLoader;
        }

        public void BuyItem(PlayerItemData item)
        {
            item.Unlocked = true;
            playerItemLoader.Save(item);

            UseItem(item);
        }

        public void UseItem(PlayerItemData item)
        {
            playerLookData.CurrentBodyId = item.Id;
            playerLookLoader.Save(playerLookData);

            if (OnBodyChanged != null) OnBodyChanged(item);
        }
    }
}
