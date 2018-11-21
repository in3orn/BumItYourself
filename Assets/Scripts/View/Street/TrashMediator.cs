using System;
using System.Collections.Generic;
using Krk.Bum.Game.Context;
using Krk.Bum.Game.Items;
using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class TrashMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private ViewContext viewContext = null;

        [SerializeField]
        private TrashConfig trashConfig;


        private ModelController modelController;
        private BlocksController blocksController;


        private List<TrashView> views;
        private List<TrashController> controllers;


        private void Awake()
        {
            views = new List<TrashView>();
            controllers = new List<TrashController>();

            modelController = modelContext.ModelController;
            blocksController = viewContext.TrashBlocksController;
        }

        private void OnEnable()
        {
            blocksController.OnBlockSpawned += HandleBlockSpawned;
        }

        private void HandleBlockSpawned(BlockView blockView, BlockData data)
        {
            var trashView = blockView.GetComponent<TrashView>();
            views.Add(trashView);

            var trashController = new TrashController(modelController, trashConfig);
            controllers.Add(trashController);

            trashView.OnClicked += trashController.Hit;

            trashController.OnEmptyHit += trashView.HitEmpty;
            trashController.OnHit += trashView.Hit;
        }

        private void OnDisable()
        {
            //TODO
        }
    }
}
