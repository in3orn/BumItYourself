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

            if (CurrentBodyId.Length > 0)
            {
                var currentItem = GetItem(CurrentBodyId);
                currentItem.Equipped = true;
            }
        }

        public PlayerItemData GetItem(string id)
        {
            foreach(var item in Bodies)
            {
                if(id.Equals(item.Id))
                {
                    return item;
                }
            }

            return null;
        }

        public void BuyItem(PlayerItemData item)
        {
            item.Unlocked = true;
            playerItemLoader.Save(item);

            UseItem(item);
        }

        public void UseItem(PlayerItemData item)
        {
            ClearItems(playerLookData.Bodies);
            item.Equipped = true;

            playerLookData.CurrentBodyId = item.Id;
            playerLookLoader.Save(playerLookData);

            if (OnBodyChanged != null) OnBodyChanged(item);
        }

        private void ClearItems(PlayerItemData[] items)
        {
            foreach (var item in items)
            {
                item.Equipped = false;
            }
        }

        public void UpdateItemsState(PlayerItemData[] items, int cash)
        {
            foreach (var item in items)
            {
                item.CanPurchase = item.Price <= cash;
            }
        }
    }
}
