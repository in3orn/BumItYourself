﻿using UnityEngine;
using Krk.Bum.View.Model;

namespace Krk.Bum.View.Screens
{
    public class StreetScreenMediator : ScreenMediator
    {
        [SerializeField]
        private StreetScreenView screenView = null;


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
        }

        private void HandleSettingsButtonClicked()
        {
            //TODO revert back viewStateController.SetState(ViewStateEnum.Settings);
            viewStateController.SetState(ViewStateEnum.Store);
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
    }
}
