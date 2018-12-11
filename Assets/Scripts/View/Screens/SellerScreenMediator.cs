using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;

namespace Krk.Bum.View.Screens
{
    public class SellerScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private SellerScreenView screenView = null;


        private PlayerLookController playerLookController;

        private ModelController modelController;

        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
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
                screenView.Init(playerLookController.Bodies);
                Subscribe();
            }
            base.SetShown(shown);
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
                button.OnButtonClicked += HandleCollectionButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked -= HandleCollectionButtonClicked;
            }
        }

        private void HandleCollectionButtonClicked(SellerItemButton button)
        {
            var item = button.Item;
            if(item.Unlocked)
            {
                playerLookController.UseItem(item);
            }
            else if (modelController.Cash >= item.Price)
            {
                playerLookController.BuyItem(item);
                modelController.DecreaseMoney(item.Price);
            }

            button.UpdateAppearance();
        }
    }
}
