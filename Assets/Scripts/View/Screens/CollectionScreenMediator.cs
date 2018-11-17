using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;

namespace Krk.Bum.View.Buttons
{
    public class CollectionScreenMediator : ScreenMediator
    {
        [SerializeField]
        private CollectionScreenView screenView = null;


        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
            backListener = viewContext.BackButtonListener;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);
            screenView.OnTestButtonClicked += HandleTestButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
                screenView.OnTestButtonClicked -= HandleTestButtonClicked;
            }
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Item);
        }
    }
}
