using Krk.Bum.View.Actors;
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

        private BlocksController blocksController;

        public BlocksController BlocksController
        {
            get
            {
                return blocksController ?? (blocksController =
                    new BlocksController(BlocksControllerConfig));
            }
        }

        public ThoughtsProviderConfig StartThoughtsProviderConfig;

        private ThoughtsProvider startThoughtsProvider;

        public ThoughtsProvider StartThoughtsProvider
        {
            get
            {
                return startThoughtsProvider ?? (startThoughtsProvider =
                    new ThoughtsProvider(StartThoughtsProviderConfig));
            }
        }

        public ThoughtsProviderConfig CollectionThoughtsProviderConfig;

        private ThoughtsProvider collectionThoughtsProvider;

        public ThoughtsProvider CollectionThoughtsProvider
        {
            get
            {
                return collectionThoughtsProvider ?? (collectionThoughtsProvider =
                    new ThoughtsProvider(CollectionThoughtsProviderConfig));
            }
        }

        public ThoughtsProviderConfig NotificationThoughtsProviderConfig;

        private ThoughtsProvider notificationThoughtsProvider;

        public ThoughtsProvider NotificationThoughtsProvider
        {
            get
            {
                return notificationThoughtsProvider ?? (notificationThoughtsProvider =
                    new ThoughtsProvider(NotificationThoughtsProviderConfig));
            }
        }
    }
}
