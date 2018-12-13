using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;
using Krk.Bum.View.Street;
using System.Collections.Generic;
using Krk.Bum.Model;
using Krk.Bum.Game.Items;

namespace Krk.Bum.View.Screens
{
    public class SellerScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private SellerScreenView screenView = null;


        private SellerViewController sellerViewController;
        private PlayerLookController playerLookController;

        private ModelController modelController;

        private IButtonListener backListener;


        private readonly List<PlayerItemData> sellerItems;


        public SellerScreenMediator()
        {
            sellerItems = new List<PlayerItemData>();
        }


        protected override void Awake()
        {
            base.Awake();

            sellerViewController = viewContext.SellerViewController;
            playerLookController = modelContext.PlayerLookController;
            modelController = modelContext.ModelController;
            backListener = viewContext.BackButtonListener;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                Unsubscribe();

                InitSellerItems();
                UpdateItemsState(modelController.Cash);
                screenView.Init(sellerItems.ToArray());

                Subscribe();
            }

            base.SetShown(shown);
        }

        private void InitSellerItems()
        {
            sellerItems.Clear();

            foreach (var group in sellerViewController.CurrentConfig.Groups)
            {
                AppendByCollectionItems(group);
                AppendByBodyItems(group);
                AppendByBagItems(group);
                AppendByStickItems(group);
                AppendByGlassesItems(group);
            }
        }

        private void AppendByCollectionItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.CollectionsIds)
            {
                var collection = modelController.GetCollection(id);
                sellerItems.Add(new PlayerItemData()
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    Image = collection.Image,
                    Price = collection.Price,
                    Unlocked = collection.Unlocked,
                    Equipped = collection.Unlocked
                });
            }
        }

        private void AppendByBodyItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.BodiesIds)
            {
                sellerItems.Add(playerLookController.GetBody(id));
            }
        }

        private void AppendByBagItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.BagsIds)
            {
                sellerItems.Add(playerLookController.GetBag(id));
            }
        }

        private void AppendByStickItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.SticksIds)
            {
                sellerItems.Add(playerLookController.GetStick(id));
            }
        }

        private void AppendByGlassesItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.GlassesIds)
            {
                sellerItems.Add(playerLookController.GetGlasses(id));
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);

            Subscribe();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);

                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked += HandleSellerButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked -= HandleSellerButtonClicked;
            }
        }

        private void HandleSellerButtonClicked(SellerItemButton button)
        {
            var item = button.Item;
            if (item.Unlocked)
            {
                var collection = modelController.GetCollection(item.Id);
                if (collection == null)
                {
                    playerLookController.UseItem(item);
                }
            }
            else if (modelController.Cash >= item.Price)
            {
                var collection = modelController.GetCollection(item.Id);
                if (collection != null)
                {
                    modelController.UnlockCollection(collection);
                    modelController.DecreaseMoney(item.Price);
                    item.Unlocked = true;
                    item.Equipped = true;
                }
                else
                {
                    playerLookController.BuyItem(item);
                    modelController.DecreaseMoney(item.Price);
                }
            }
            
            UpdateItemsState(modelController.Cash);
            screenView.UpdateAppearance();
        }

        public void UpdateItemsState(int cash)
        {
            foreach (var item in sellerItems)
            {
                item.CanPurchase = item.Price <= cash;
            }
        }
    }
}
