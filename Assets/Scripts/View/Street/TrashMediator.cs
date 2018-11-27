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
        private ViewContext viewContext = null;

        [SerializeField]
        private TrashConfig trashConfig = null;


        private ModelController modelController;
        private BlocksController blocksController;


        private Dictionary<TrashView, TrashController> trashes;


        private void Awake()
        {
            trashes = new Dictionary<TrashView, TrashController>();

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
            var trashController = new TrashController(modelController, trashConfig)
            {
                Position = trashView.transform.position
            };

            trashes[trashView] = trashController;

            trashView.OnClicked += trashController.Hit;

            trashController.OnEmptyHit += trashView.HitEmpty;
            trashController.OnHit += trashView.Hit;
        }

        private void OnDisable()
        {
            //TODO
        }


        public TrashController GetControllerFor(TrashView view)
        {
            if(trashes.ContainsKey(view))
            {
                var result = trashes[view];
                result.Position = view.transform.position; //TODO hacky :(

                return result;
            }
            return null;
        }
    }
}
