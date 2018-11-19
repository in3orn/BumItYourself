using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;

namespace Krk.Bum.View.Screens
{
    public class InventoryScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private InventoryScreenView screenView = null;


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

        protected override void Start()
        {
            screenView.Init(modelController.GetAllCollections());
            Subscribe();
            base.Start();
        }

        private void Subscribe()
        {
            foreach(var button in screenView.CollectionButtons)
            {
                button.OnButtonClicked += HandleCollectionButtonClicked;
            }
        }

        private void Unsubscribe()
        {
            foreach (var button in screenView.CollectionButtons)
            {
                button.OnButtonClicked -= HandleCollectionButtonClicked;
            }
        }

        private void HandleCollectionButtonClicked(string id)
        {
            viewStateController.CurrentCollectionId = id;
            viewStateController.SetState(ViewStateEnum.Collection);
        }
    }
}
