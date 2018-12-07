using System;
using System.Collections.Generic;
using Krk.Bum.Game.Actors;
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
        private ViewContext viewContext = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private TrashConfig trashConfig = null;

        [SerializeField]
        private PartMediator partMediator = null;


        private ModelController modelController;
        private BlocksController blocksController;
        private PlayerController playerController;


        private Dictionary<TrashView, IStreetItemController> trashes;


        private void Awake()
        {
            trashes = new Dictionary<TrashView, IStreetItemController>();

            modelController = modelContext.ModelController;
            blocksController = viewContext.BlocksController;
            playerController = gameContext.PlayerController;
        }

        private void OnEnable()
        {
            blocksController.OnBlockSpawned += HandleBlockSpawned;
        }

        private void HandleBlockSpawned(BlockView blockView, BlockData data)
        {
            var trashViews = blockView.GetComponentsInChildren<TrashView>();
            foreach (var trashView in trashViews)
            {
                var trashController = new TrashController(modelController, trashConfig)
                {
                    Position = trashView.transform.position
                };

                trashes[trashView] = trashController;

                trashController.OnEmptyHit += trashView.HitEmpty;
                trashController.OnHit += trashView.Hit;
                trashController.OnHit += HandleTrashHit;

                trashView.OnHit += HandleTrashViewHit;
            }
        }

        private void OnDisable()
        {
            //TODO
        }

        private void HandleTrashHit(TrashData trashData, PartData partData)
        {
            playerController.Hit();
        }

        private void HandleTrashViewHit(TrashView trashView, PartData partData)
        {
            partMediator.Spawn(trashView, partData);
        }

        public IStreetItemController GetControllerFor(TrashView view)
        {
            if (trashes.ContainsKey(view))
            {
                var result = trashes[view];
                result.Position = view.transform.position; //TODO hacky :(

                return result;
            }
            return null;
        }
    }
}
