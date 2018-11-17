using UnityEngine;
using Krk.Bum.View.Model;

namespace Krk.Bum.View.Screens
{
    public class StreetScreenMediator : ScreenMediator
    {
        [SerializeField]
        private StreetScreenView screenView;


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

            if(screenView != null)
            {
                screenView.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
                screenView.OnPlayerButtonClicked -= HandlePlayerButtonClicked;
                screenView.OnInventoryButtonClicked -= HandleInventoryButtonClicked;
            }
        }

        private void HandleSettingsButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Settings;
        }

        private void HandlePlayerButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Player;
        }

        private void HandleInventoryButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Inventory;
        }
    }
}
