using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.Model;
using System;

namespace Krk.Bum.View.Screens
{
    public class StreetScreenMediator : ScreenMediator
    {
        [SerializeField]
        private StreetScreenView screenView = null;

        [SerializeField]
        private ModelContext modelContext = null;


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

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.OnSettingsButtonClicked += HandleSettingsButtonClicked;
            screenView.OnPlayerButtonClicked += HandlePlayerButtonClicked;
            screenView.OnInventoryButtonClicked += HandleInventoryButtonClicked;

            modelController.OnPartCollected += HandlePartcollected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
                screenView.OnPlayerButtonClicked -= HandlePlayerButtonClicked;
                screenView.OnInventoryButtonClicked -= HandleInventoryButtonClicked;
            }

            if (modelContext != null)
            {
                modelController.OnPartCollected -= HandlePartcollected;
            }
        }

        private void HandleSettingsButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Settings);
        }

        private void HandlePlayerButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Player);
        }

        private void HandleInventoryButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Inventory);
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Game);
        }

        private void HandlePartcollected(PartData partData)
        {
            screenView.AnimateInventoryButton();
        }
    }
}
