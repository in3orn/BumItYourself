using Krk.Bum.Common;
using Krk.Bum.View.Model;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class BackButtonListener : IButtonListener
    {
        private readonly ViewStateController viewStateController;


        public BackButtonListener(ViewStateController viewStateController)
        {
            this.viewStateController = viewStateController;
        }

        public void Subscribe(Button button)
        {
            button.onClick.AddListener(HandleActionInvoked);
        }

        public void Unsubscribe(Button button)
        {
            button.onClick.RemoveListener(HandleActionInvoked);
        }

        private void HandleActionInvoked()
        {
            viewStateController.BackState();
        }
    }
}
