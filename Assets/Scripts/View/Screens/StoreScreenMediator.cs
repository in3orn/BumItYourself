using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;
using System;

namespace Krk.Bum.View.Screens
{
    public class StoreScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private StoreScreenView screenView = null;


        private ModelController modelController;

        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
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
                screenView.Init(modelController.GetCreatedItems().ToArray());
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
            screenView.SellAllButton.onClick.AddListener(HandleSellAllClicked);

            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked += HandleCollectionButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            screenView.SellAllButton.onClick.RemoveListener(HandleSellAllClicked);

            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked -= HandleCollectionButtonClicked;
            }
        }

        private void HandleSellAllClicked()
        {
            modelController.SellAllItems();

            screenView.UpdateAppearance();
        }

        private void HandleCollectionButtonClicked(StoreItemButton button)
        {
            var item = button.Item;
            if (modelController.CanSellItem(item))
            {
                modelController.SellItem(item);
            }

            button.UpdateAppearance();
        }
    }
}
