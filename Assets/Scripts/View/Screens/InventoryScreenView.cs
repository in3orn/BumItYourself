using System;
using Krk.Bum.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class InventoryScreenView : ScreenView
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


        public void InitCollections(CollectionData[] collectionData)
        {
            Debug.Log("Collections: " + collectionData.Length);
        }
    }
}
