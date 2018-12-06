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
                var item = modelController.GetItem(viewStateController.CurrentItemId);

                InitView(item);
            }
            base.SetShown(shown);
        }

        private void InitView(ItemData item)
        {
            var parts = modelController.GetRequiredParts(item);
            var canCreate = modelController.CanCreateItem(item);
            var hasPrev = modelController.HasPrevItem(item.Id);
            var hasNext = modelController.HasNextItem(item.Id);
            screenView.InitItem(item, parts, canCreate, hasPrev, hasNext);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);

            screenView.CreateButton.onClick.AddListener(HandleCreateButtonClicked);
            screenView.PrevItemButton.onClick.AddListener(HandlePrevItemButtonClicked);
            screenView.NextItemButton.onClick.AddListener(HandleNextItemButtonClicked);
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
            var item = modelController.GetItem(viewStateController.CurrentItemId);

            if (modelController.CanCreateItem(item))
            {
                modelController.CreateItem(item);
                InitView(item);
            }
        }

        private void HandlePrevItemButtonClicked()
        {
            var item = modelController.GetPrevItem(viewStateController.CurrentItemId);
            if (item != null)
            {
                viewStateController.CurrentItemId = item.Id;
                SwitchView(item);
            }
        }

        private void HandleNextItemButtonClicked()
        {
            var item = modelController.GetNextItem(viewStateController.CurrentItemId);
            if (item != null)
            {
                viewStateController.CurrentItemId = item.Id;
                SwitchView(item);
            }
        }

        private void UpdateView(ItemData item)
        {
            screenView.UpdateItem(item);
        }

        private void SwitchView(ItemData item)
        {
            var parts = modelController.GetRequiredParts(item);
            var canCreate = modelController.CanCreateItem(item);
            var hasPrev = modelController.HasPrevItem(item.Id);
            var hasNext = modelController.HasNextItem(item.Id);
            screenView.SwitchItem(item, parts, canCreate, hasPrev, hasNext);
        }
    }
}
