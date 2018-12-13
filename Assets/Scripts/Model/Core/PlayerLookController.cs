using Krk.Bum.Model.Utils;
using UnityEngine.Events;

namespace Krk.Bum.Model.Core
{
    public class PlayerLookController
    {
        public UnityAction<PlayerItemData> OnBodyChanged;
        public UnityAction<PlayerItemData> OnBagChanged;
        public UnityAction<PlayerItemData> OnStickChanged;


        private readonly PlayerLookData playerLookData;

        private readonly PlayerLookLoader playerLookLoader;

        private readonly PlayerItemLoader playerItemLoader;


        public string CurrentBodyId { get { return playerLookData.CurrentBodyId; } }
        public string CurrentBagId { get { return playerLookData.CurrentBagId; } }
        public string CurrentStickId { get { return playerLookData.CurrentStickId; } }

        public PlayerItemData[] Bodies { get { return playerLookData.Bodies; } }
        public PlayerItemData[] Bags { get { return playerLookData.Bags; } }
        public PlayerItemData[] Sticks { get { return playerLookData.Sticks; } }


        public PlayerLookController(PlayerLookData playerLookData,
            PlayerLookLoader playerLookLoader,
            PlayerItemLoader playerItemLoader)
        {
            this.playerLookData = playerLookData;
            this.playerLookLoader = playerLookLoader;
            this.playerItemLoader = playerItemLoader;

            if (CurrentBodyId.Length > 0)
            {
                var currentItem = GetBody(CurrentBodyId);
                currentItem.Equipped = true;
            }

            if (CurrentBagId.Length > 0)
            {
                var currentItem = GetBag(CurrentBagId);
                currentItem.Equipped = true;
            }

            if (CurrentStickId.Length > 0)
            {
                var currentItem = GetStick(CurrentStickId);
                currentItem.Equipped = true;
            }
        }

        public PlayerItemData GetItem(string id)
        {
            var result = GetBody(id);
            if (result != null) return result;

            result = GetBag(id);
            if (result != null) return result;

            result = GetStick(id);
            if (result != null) return result;

            return null;
        }

        public PlayerItemData GetBody(string id)
        {
            return GetItem(Bodies, id);
        }

        public PlayerItemData GetBag(string id)
        {
            return GetItem(Bags, id);
        }

        public PlayerItemData GetStick(string id)
        {
            return GetItem(Sticks, id);
        }

        public PlayerItemData GetItem(PlayerItemData[] items, string id)
        {
            foreach (var item in items)
            {
                if (id.Equals(item.Id))
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
            item.Equipped = true;

            if (Contains(playerLookData.Bodies, item))
            {
                ClearItems(playerLookData.Bodies);
                item.Equipped = true;

                playerLookData.CurrentBodyId = item.Id;
                playerLookLoader.Save(playerLookData);

                if (OnBodyChanged != null) OnBodyChanged(item);
                return;
            }
            if (Contains(playerLookData.Bags, item))
            {
                ClearItems(playerLookData.Bags);
                item.Equipped = true;

                playerLookData.CurrentBagId = item.Id;
                playerLookLoader.Save(playerLookData);

                if (OnBagChanged != null) OnBagChanged(item);
                return;
            }
            if (Contains(playerLookData.Sticks, item))
            {
                ClearItems(playerLookData.Sticks);
                item.Equipped = true;

                playerLookData.CurrentStickId = item.Id;
                playerLookLoader.Save(playerLookData);

                if (OnStickChanged != null) OnStickChanged(item);
                return;
            }
        }

        private bool Contains(PlayerItemData[] items, PlayerItemData searched)
        {
            foreach (var item in items)
            {
                if (item.Id.Equals(searched.Id)) return true;
            }

            return false;
        }

        private void ClearItems(PlayerItemData[] items)
        {
            foreach (var item in items)
            {
                item.Equipped = false;
            }
        }
    }
}
