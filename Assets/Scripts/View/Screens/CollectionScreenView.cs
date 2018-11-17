using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class CollectionScreenView : ScreenView
    {
        public UnityAction OnBackButtonClicked;
        public UnityAction OnTestButtonClicked;


        [SerializeField]
        private Button backButton;

        [SerializeField]
        private Button testButton;


        private void OnEnable()
        {
            backButton.onClick.AddListener(HandleBackClicked);
            testButton.onClick.AddListener(HandleTestButtonClicked);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(HandleBackClicked);
            testButton.onClick.RemoveListener(HandleTestButtonClicked);
        }

        private void HandleBackClicked()
        {
            if (OnBackButtonClicked != null) OnBackButtonClicked();
        }

        private void HandleTestButtonClicked()
        {
            if (OnTestButtonClicked != null) OnTestButtonClicked();
        }
    }
}
