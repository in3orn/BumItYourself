using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class IntroScreenMediator : ScreenMediator
    {
        [SerializeField]
        private IntroScreenView screenView = null;


        protected override void OnEnable()
        {
            screenView.OnIntroEnded += HandleExitButtonClicked;

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.OnIntroEnded -= HandleExitButtonClicked;
            }
        }

        private void HandleExitButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Street);
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }
    }
}
