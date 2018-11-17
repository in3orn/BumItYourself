using UnityEngine;
using Krk.Bum.View.Model;
using System;

namespace Krk.Bum.View.Screens
{
    public class InventoryScreenMediator : ScreenMediator
    {
        [SerializeField]
        private InventoryScreenView screenView;


        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.OnBackButtonClicked += HandleBackButtonClicked;
            screenView.OnTestButtonClicked += HandleTestButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(screenView != null)
            {
                screenView.OnBackButtonClicked -= HandleBackButtonClicked;
                screenView.OnTestButtonClicked -= HandleTestButtonClicked;
            }
        }

        private void HandleBackButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Street;
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Collection;
        }
    }
}
