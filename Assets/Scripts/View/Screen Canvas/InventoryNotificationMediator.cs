using System;
using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class InventoryNotificationMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext;

        [SerializeField]
        private NotificationView notificationView;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }

        private void Start()
        {
            if(modelController.CanCreateAnyItem())
            {
                notificationView.Init();
            }
        }

        private void OnEnable()
        {
            modelController.OnPartCollected += HandlePartCollected;
            modelController.OnItemCreated += HandleItemCreated;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                modelController.OnPartCollected -= HandlePartCollected;
                modelController.OnItemCreated -= HandleItemCreated;
            }
        }

        private void HandlePartCollected(PartData part)
        {
            if (!notificationView.Shown && modelController.CanCreateAnyItem())
            {
                notificationView.Show();
            }
        }

        private void HandleItemCreated(ItemData item)
        {
            if (notificationView.Shown && !modelController.CanCreateAnyItem())
            {
                notificationView.Hide();
            }
        }
    }
}
