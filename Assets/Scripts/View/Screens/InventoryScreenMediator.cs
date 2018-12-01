using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;
using System;

namespace Krk.Bum.View.Screens
{
    public class InventoryScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private InventoryScreenView screenView = null;


        private ModelController modelController;


        protected override void Awake()
        {
            base.Awake();
            modelController = modelContext.ModelController;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                screenView.Update();
            }
            base.SetShown(shown);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.BackButton.onClick.AddListener(HandleBackClicked);

            Subscribe();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                screenView.BackButton.onClick.RemoveListener(HandleBackClicked);

                Unsubscribe();
            }
        }

        protected override void Start()
        {
            screenView.Init(modelController.GetAllCollections());

            Subscribe();
            base.Start();
        }

        private void Subscribe()
        {
            foreach (var collectionView in screenView.CollectionViews.Values)
            {
                Subscribe(collectionView);
            }
        }

        private void Subscribe(CollectionView collectionView)
        {
            foreach (var itemButton in collectionView.ItemButtons)
            {
                itemButton.OnButtonClicked += HandleItemButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            foreach (var collectionView in screenView.CollectionViews.Values)
            {
                Unsubscribe(collectionView);
            }
        }

        private void Unsubscribe(CollectionView collectionView)
        {
            foreach (var itemButton in collectionView.ItemButtons)
            {
                itemButton.OnButtonClicked -= HandleItemButtonClicked;
            }
        }

        private void HandleBackClicked()
        {
            viewStateController.BackState(ViewStateEnum.Inventory);
        }

        private void HandleItemButtonClicked(ItemButton button)
        {
            viewStateController.CurrentItemId = button.Item.Id;
            viewStateController.SetState(ViewStateEnum.Item);
        }
    }
}
