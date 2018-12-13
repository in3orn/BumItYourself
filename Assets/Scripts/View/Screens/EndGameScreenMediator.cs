using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.Model;
using System;

namespace Krk.Bum.View.Screens
{
    public class EndGameScreenMediator : ScreenMediator
    {
        [SerializeField]
        private EndGameScreenView screenView = null;

        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ItemConfig[] endGameItems = null;


        private IButtonListener backListener;

        private ModelController modelController;


        protected override void Awake()
        {
            base.Awake();
            backListener = viewContext.BackButtonListener;
            modelController = modelContext.ModelController;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);

            modelController.OnItemCreated += HandleItemCreated;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
            }

            if(modelContext != null)
            {
                modelController.OnItemCreated -= HandleItemCreated;
            }
        }

        private void HandleItemCreated(ItemData data)
        {
            foreach (var item in endGameItems)
            {
                if (item.Id.Equals(data.Id) && data.TotalCount == 1)
                {
                    screenView.SetAwardImage(data.Image);
                    viewStateController.SetState(Model.ViewStateEnum.EndGame);
                }
            }
        }
    }
}
