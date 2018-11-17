using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class CollectionScreenView : ScreenView
    {
        public UnityAction OnTestButtonClicked;


        public Button BackButton;

        [SerializeField]
        private Button testButton = null;


        private void OnEnable()
        {
            testButton.onClick.AddListener(HandleTestButtonClicked);
        }

        private void OnDisable()
        {
            testButton.onClick.RemoveListener(HandleTestButtonClicked);
        }

        private void HandleTestButtonClicked()
        {
            if (OnTestButtonClicked != null) OnTestButtonClicked();
        }
    }
}
