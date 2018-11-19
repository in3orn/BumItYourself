using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.Model;
using System;

namespace Krk.Bum.View.Screens
{
    public class ItemScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ItemScreenView screenView = null;


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
                var collectionId = viewStateController.CurrentCollectionId;
                var itemId = viewStateController.CurrentItemId;
                var item = modelController.GetItem(collectionId, itemId);

                InitView(item);
            }
            base.SetShown(shown);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);

            screenView.CreateButton.onClick.AddListener(HandleCreateButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
            }

            if (screenView != null)
            {
                screenView.CreateButton.onClick.RemoveListener(HandleCreateButtonClicked);
            }
        }

        private void HandleCreateButtonClicked()
        {
            var collectionId = viewStateController.CurrentCollectionId;
            var itemId = viewStateController.CurrentItemId;
            var item = modelController.GetItem(collectionId, itemId);

            if (modelController.CanCreateItem(item))
            {
                modelController.CreateItem(item);
                InitView(item);
            }
        }

        private void InitView(ItemData item)
        {
            var parts = modelController.GetRequiredParts(item);
            var canCreate = modelController.CanCreateItem(item);
            screenView.Init(item, parts, canCreate);
        }
    }
}
