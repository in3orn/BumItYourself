using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public class SettingsScreenMediator : ScreenMediator
    {
        [SerializeField]
        private SettingsScreenView screenView = null;


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
