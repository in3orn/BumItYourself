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
        private SellerItemButtonConfig config = null;

        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private Image background = null;

        [SerializeField]
        private TextMeshProUGUI price = null;


        public PlayerItemData Item { get; private set; }


        public void Init(PlayerItemData data)
        {
            Item = data;

            UpdateAppearance();

            image.sprite = data.Image.Image;
            image.color = data.Image.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Image.Rotation);
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
            if(Item.Equipped)
            {
                price.text = "use";
                background.color = config.EquippedColor;
                button.interactable = true;
            }
            else if(Item.Unlocked)
            {
                price.text = "use";
                background.color = config.PurchasedColor;
                button.interactable = true;
            }
            else if(Item.CanPurchase)
            {
                price.text = string.Format(config.PriceFormat, Item.Price);
                background.color = config.DefaultColor;
                button.interactable = true;
            }
            else
            {
                price.text = string.Format(config.PriceFormat, Item.Price);
                background.color = config.LockedColor;
                button.interactable = false;
            }
        }
    }
}
