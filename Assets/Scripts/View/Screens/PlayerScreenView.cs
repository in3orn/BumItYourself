using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class PlayerScreenView : ScreenView
    {
        public UnityAction OnBackButtonClicked;


        [SerializeField]
        private Button backButton;


        private void OnEnable()
        {
            backButton.onClick.AddListener(HandleBackClicked);
        }

        private void OnDisable()
        {
            backButton.onClick.AddListener(HandleBackClicked);
        }

        private void HandleBackClicked()
        {
            if (OnBackButtonClicked != null) OnBackButtonClicked();
        }
    }
}
