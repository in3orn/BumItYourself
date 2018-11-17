using UnityEngine;
using Krk.Bum.View.Core;

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
            screenView.OnItemsButtonClicked += HandleItemsButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(screenView != null)
            {
                screenView.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
                screenView.OnPlayerButtonClicked -= HandlePlayerButtonClicked;
                screenView.OnItemsButtonClicked -= HandleItemsButtonClicked;
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

        private void HandleItemsButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Items;
        }
    }
}
