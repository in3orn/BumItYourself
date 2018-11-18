using System;
using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public class SummaryScreenMediator : ScreenMediator
    {
        [SerializeField]
        private SummaryScreenView screenView = null;


        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.CollectButton.onClick.AddListener(HandleCollectButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.CollectButton.onClick.RemoveListener(HandleCollectButtonClicked);
            }
        }

        private void HandleCollectButtonClicked()
        {
            viewStateController.SetState(Model.ViewStateEnum.Street);
        }
    }
}
