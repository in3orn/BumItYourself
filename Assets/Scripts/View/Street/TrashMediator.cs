using System.Collections.Generic;
using Krk.Bum.Game.Items;
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


        private Dictionary<TrashView, IStreetItemController> trashes;


        private void Awake()
        {
            trashes = new Dictionary<TrashView, IStreetItemController>();

            modelController = modelContext.ModelController;
            blocksController = viewContext.BlocksController;
        }

        private void OnEnable()
        {
            blocksController.OnBlockSpawned += HandleBlockSpawned;
        }

        private void HandleBlockSpawned(BlockView blockView, BlockData data)
        {
            var trashViews = blockView.GetComponentsInChildren<TrashView>();
            foreach(var trashView in trashViews)
            {
                var trashController = new TrashController(modelController, trashConfig)
                {
                    Position = trashView.transform.position
                };

                trashes[trashView] = trashController;

                trashController.OnEmptyHit += trashView.HitEmpty;
                trashController.OnHit += trashView.Hit;
            }
        }

        private void OnDisable()
        {
            //TODO
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
