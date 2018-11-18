﻿using UnityEngine;
using Krk.Bum.View.Model;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;

namespace Krk.Bum.View.Buttons
{
    public class CollectionScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private CollectionScreenView screenView = null;


        private ModelController modelController;

        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
            modelController = modelContext.ModelController;
            backListener = viewContext.BackButtonListener;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                var collection = modelController.GetCollection(viewStateController.CurrentCollectionId);
                Debug.Log("Items: " + collection.Items.Length);
            }
            base.SetShown(shown);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);
            screenView.OnTestButtonClicked += HandleTestButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
                screenView.OnTestButtonClicked -= HandleTestButtonClicked;
            }
        }

        private void HandleTestButtonClicked()
        {
            viewStateController.SetState(ViewStateEnum.Item);
        }
    }
}
