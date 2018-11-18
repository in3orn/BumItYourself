using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;

namespace Krk.Bum.View.Buttons
{
    public class ItemScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ItemScreenView screenView = null;


        private ModelController modelController;

        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
            modelController = modelContext.ModelController;
            backListener = viewContext.BackButtonListener;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                var collectionId = viewStateController.CurrentCollectionId;
                var itemId = viewStateController.CurrentItemId;
                var item = modelController.GetItem(collectionId, itemId);
                
                screenView.Init(item);
            }
            base.SetShown(shown);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
            }
        }
    }
}
