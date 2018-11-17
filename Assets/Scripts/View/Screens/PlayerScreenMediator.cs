using UnityEngine;
using Krk.Bum.View.Model;

namespace Krk.Bum.View.Screens
{
    public class PlayerScreenMediator : ScreenMediator
    {
        [SerializeField]
        private PlayerScreenView screenView;


        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.OnBackButtonClicked += HandleBackButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(screenView != null)
            {
                screenView.OnBackButtonClicked -= HandleBackButtonClicked;
            }
        }

        private void HandleBackButtonClicked()
        {
            viewStateController.State = ViewStateEnum.Street;
        }
    }
}
