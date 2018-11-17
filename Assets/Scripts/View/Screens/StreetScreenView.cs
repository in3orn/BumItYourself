using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class StreetScreenView : ScreenView
    {
        public UnityAction OnSettingsButtonClicked;
        public UnityAction OnPlayerButtonClicked;
        public UnityAction OnItemsButtonClicked;


        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button playerButton;

        [SerializeField]
        private Button itemsButton;


        private void OnEnable()
        {
            settingsButton.onClick.AddListener(HandleSettingsClicked);
            playerButton.onClick.AddListener(HandlePlayerClicked);
            itemsButton.onClick.AddListener(HandleItemsClicked);
        }

        private void OnDisable()
        {
            settingsButton.onClick.RemoveListener(HandleSettingsClicked);
            playerButton.onClick.RemoveListener(HandlePlayerClicked);
            itemsButton.onClick.RemoveListener(HandleItemsClicked);
        }

        private void HandleSettingsClicked()
        {
            if (OnSettingsButtonClicked != null) OnSettingsButtonClicked();
        }

        private void HandlePlayerClicked()
        {
            if (OnPlayerButtonClicked != null) OnPlayerButtonClicked();
        }

        private void HandleItemsClicked()
        {
            if (OnItemsButtonClicked != null) OnItemsButtonClicked();
        }
    }
}
