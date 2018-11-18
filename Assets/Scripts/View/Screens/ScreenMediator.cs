using Krk.Bum.View.Context;
using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public abstract class ScreenMediator : MonoBehaviour
    {
        [SerializeField]
        private ScreenMediatorConfig config = null;

        [SerializeField]
        protected ViewContext viewContext = null;


        protected ViewStateController viewStateController;


        protected virtual void Awake()
        {
            viewStateController = viewContext.ViewStateController;
        }

        protected virtual void Start()
        {
            GetScreenView().InitShown(ShouldShow(viewStateController.State));
        }

        protected virtual void OnEnable()
        {
            viewStateController.OnStateChanged += HandleViewStateChanged;
        }

        protected virtual void OnDisable()
        {
            if (viewContext != null)
            {
                viewStateController.OnStateChanged -= HandleViewStateChanged;
            }
        }

        private void HandleViewStateChanged(ViewStateEnum state)
        {
            SetShown(ShouldShow(state));
        }

        protected virtual void SetShown(bool shown)
        {
            GetScreenView().SetShown(shown);
        }

        private bool ShouldShow(ViewStateEnum state)
        {
            foreach (var showScreenState in config.ShowScreenStates)
            {
                if (state == showScreenState) return true;
            }

            return false;
        }

        protected abstract ScreenView GetScreenView();
    }
}
