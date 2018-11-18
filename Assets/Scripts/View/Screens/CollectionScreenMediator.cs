using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;

namespace Krk.Bum.View.Buttons
{
    public class CollectionScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private CollectionScreenView screenView = null;


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

        private void HandleCollectionButtonClicked(string id)
        {
            viewStateController.CurrentItemId = id;
            viewStateController.SetState(ViewStateEnum.Item);
        }
    }
}
