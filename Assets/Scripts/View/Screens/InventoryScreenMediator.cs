using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;

namespace Krk.Bum.View.Buttons
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
            screenView.OnTestButtonClicked += HandleTestButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
                screenView.OnTestButtonClicked -= HandleTestButtonClicked;
            }
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Collection);
        }


        protected override void Start()
        {
            screenView.InitCollections(modelController.GetAllCollections());
        }
    }
}
