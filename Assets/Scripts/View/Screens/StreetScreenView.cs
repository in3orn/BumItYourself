using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class StreetScreenView : ScreenView
    {
        public UnityAction OnSettingsButtonClicked;
        public UnityAction OnPlayerButtonClicked;
        public UnityAction OnInventoryButtonClicked;


        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button playerButton;

        [SerializeField]
        private Button inventoryButton;


        private void OnEnable()
        {
            settingsButton.onClick.AddListener(HandleSettingsClicked);
            playerButton.onClick.AddListener(HandlePlayerClicked);
            inventoryButton.onClick.AddListener(HandleItemsClicked);
        }

        private void OnDisable()
        {
            settingsButton.onClick.RemoveListener(HandleSettingsClicked);
            playerButton.onClick.RemoveListener(HandlePlayerClicked);
            inventoryButton.onClick.RemoveListener(HandleItemsClicked);
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
            if (OnInventoryButtonClicked != null) OnInventoryButtonClicked();
        }
    }
}
