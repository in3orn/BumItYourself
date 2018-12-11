using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class SellerItemButton : MonoBehaviour
    {
        public UnityAction<SellerItemButton> OnButtonClicked;


        [SerializeField]
        private StoreItemButtonConfig config = null;

        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI count = null;


        public PlayerItemData Item { get; private set; }


        public void Init(PlayerItemData data)
        {
            Item = data;
            UpdateAppearance();
            image.sprite = data.Image;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(HandleButtonClicked);
            }
        }

        private void HandleButtonClicked()
        {
            OnButtonClicked?.Invoke(this);
        }

        public void UpdateAppearance()
        {
            //TODO
            //var active = Item.Count > 0;
            //count.text = Item.Count.ToString();
            //count.color = active ? config.DefaultColor : config.LockedColor;
            //button.interactable = active;
        }
    }
}
