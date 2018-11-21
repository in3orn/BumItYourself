using Krk.Bum.View.Buttons;
using Krk.Bum.View.Model;
using Krk.Bum.View.Street;
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

        public BlocksControllerConfig BlocksControllerConfig;

        public BlocksController blocksController;

        public BlocksController BlocksController
        {
            get
            {
                return blocksController ?? (blocksController = new BlocksController(BlocksControllerConfig));
            }
        }
    }
}
