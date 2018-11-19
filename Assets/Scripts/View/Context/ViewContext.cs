using Krk.Bum.View.Buttons;
using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Context
{
    public class ViewContext : MonoBehaviour
    {
        private ViewStateController viewStateController;

        public ViewStateController ViewStateController
        {
            get
            {
                if (viewStateController == null)
                {
                    viewStateController = new ViewStateController();
                }
                return viewStateController;
            }
        }


        private BackButtonListener backButtonListener;

        public BackButtonListener BackButtonListener
        {
            get
            {
                if (backButtonListener == null)
                {
                    backButtonListener = new BackButtonListener(ViewStateController);
                }
                return backButtonListener;
            }
        }
    }
}
