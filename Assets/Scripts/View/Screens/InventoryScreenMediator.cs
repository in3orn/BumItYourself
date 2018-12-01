using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;
using Krk.Bum.Model;

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

        protected override void Start()
        {
            screenView.Init(modelController.GetAllCollections());

            Subscribe();
            base.Start();
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            base.SetShown(shown);

            if (shown)
            {
                screenView.Update();
                UpdateNotifications();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.BackButton.onClick.AddListener(HandleBackClicked);

            Subscribe();

            modelController.OnItemCreated += HandleItemCreated;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                screenView.BackButton.onClick.RemoveListener(HandleBackClicked);

                Unsubscribe();
            }

            if (modelContext != null)
            {
                modelController.OnItemCreated -= HandleItemCreated;
            }
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
            collectionView.OnExpanded += HandleCollectionExpanded;
            foreach (var itemButton in collectionView.ItemButtons)
            {
                itemButton.OnButtonClicked += HandleItemButtonClicked;
            }
        }

        private void HandleCollectionExpanded(bool expanded)
        {
            screenView.Rebuild();
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

        private void HandleItemCreated(ItemData item)
        {
            UpdateNotifications();
        }

        private void UpdateNotifications()
        {
            foreach (var pair in screenView.CollectionViews)
            {
                if (pair.Key.Unlocked)
                {
                    UpdateNotifications(pair.Value);
                }
            }
        }

        private void UpdateNotifications(CollectionView collectionView)
        {
            var collectionShown = false;

            foreach (var button in collectionView.ItemButtons)
            {
                var shown = modelController.CanCreateItem(button.Item);
                button.SetNotificationShown(shown);
                collectionShown |= shown;
            }

            collectionView.SetNotificationShown(collectionShown);
        }
    }
}
