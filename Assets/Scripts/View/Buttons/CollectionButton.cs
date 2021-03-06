﻿using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class CollectionButton : MonoBehaviour
    {
        public UnityAction<string> OnButtonClicked;


        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI title = null;


        private string id;


        public void Init(CollectionData data)
        {
            id = data.Id;
            title.text = data.Name;
            InitImage(data.Image);
        }

        private void InitImage(ImageData data)
        {
            image.sprite = data.Image;
            image.color = data.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
            image.SetNativeSize();
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
            OnButtonClicked?.Invoke(id);
        }
    }
}
