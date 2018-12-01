using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Buttons;
using System;

namespace Krk.Bum.View.Screens
{
    public class CollectionScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private CollectionScreenView screenView = null;


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
                var id = viewStateController.CurrentCollectionId;
                var collection = modelController.GetCollection(id);

                Unsubscribe();
                screenView.Init(collection.Items);
                Subscribe();
            }
            base.SetShown(shown);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Subscribe();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            screenView.BackButton.onClick.AddListener(HandleBackClicked);

            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked += HandleCollectionButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            screenView.BackButton.onClick.RemoveListener(HandleBackClicked);

            foreach (var button in screenView.ItemButtons)
            {
                button.OnButtonClicked -= HandleCollectionButtonClicked;
            }
        }

        private void HandleBackClicked()
        {
            viewStateController.BackState(ViewStateEnum.Collection);
        }

        private void HandleCollectionButtonClicked(ItemButton button)
        {
            viewStateController.CurrentItemId = button.Item.Id;
            viewStateController.SetState(ViewStateEnum.Item);
        }
    }
}
