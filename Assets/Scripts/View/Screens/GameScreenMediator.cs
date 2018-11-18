using System;
using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public class GameScreenMediator : ScreenMediator
    {
        [SerializeField]
        private GameScreenView screenView = null;


        protected override void Awake()
        {
            base.Awake();
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.TestButton.onClick.AddListener(HandleTestButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.TestButton.onClick.RemoveListener(HandleTestButtonClicked);
            }
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.SetState(Model.ViewStateEnum.Summary);
        }
    }
}
