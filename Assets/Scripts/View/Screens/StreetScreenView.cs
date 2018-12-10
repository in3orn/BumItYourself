using DG.Tweening;
using Krk.Bum.View.Animations;
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
        private Button settingsButton = null;

        [SerializeField]
        private Button playerButton = null;

        [SerializeField]
        private Button inventoryButton = null;

        [SerializeField]
        private StreetScreenConfig config = null;


        private PunchAnimationData inventoryButtonPunchData = null;


        private void Awake()
        {
            inventoryButtonPunchData = new PunchAnimationData { StartValue = Vector3.one };
        }

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

        public void AnimateInventoryButton()
        {
            var rectTransform = inventoryButton.GetComponent<RectTransform>();
            rectTransform.DOPunchScale(config.InventoryButtonPunchAnimation, inventoryButtonPunchData);
        }
    }
}
