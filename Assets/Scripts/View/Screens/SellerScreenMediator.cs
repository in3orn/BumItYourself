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
            }
        }

        private void AppendByCollectionItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.CollectionsIds)
            {
                //sellerItems.Add();
            }
        }

        private void AppendByBodyItems(SellerGroupData groupData)
        {
            foreach (var id in groupData.BodiesIds)
            {
                sellerItems.Add(playerLookController.GetItem(id));
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
                playerLookController.UseItem(item);
            }
            else if (modelController.Cash >= item.Price)
            {
                playerLookController.BuyItem(item);
                modelController.DecreaseMoney(item.Price);
            }

            //TODO do this on view items
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
