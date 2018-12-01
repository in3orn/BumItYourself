using System;
using System.Collections.Generic;
using Krk.Bum.Game.Items;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class StoreMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ViewContext viewContext = null;


        private ModelController modelController;
        private BlocksController blocksController;
        private ViewStateController viewStateController;


        private Dictionary<StoreView, IStreetItemController> trashes;


        private void Awake()
        {
            trashes = new Dictionary<StoreView, IStreetItemController>();

            modelController = modelContext.ModelController;
            blocksController = viewContext.StoreBlocksController;
            viewStateController = viewContext.ViewStateController;
        }

        private void OnEnable()
        {
            blocksController.OnBlockSpawned += HandleBlockSpawned;
        }

        private void HandleBlockSpawned(BlockView blockView, BlockData data)
        {
            var storeView = blockView.GetComponent<StoreView>();
            var storeController = new StoreController()
            {
                Position = storeView.transform.position
            };

            trashes[storeView] = storeController;

            storeController.OnStoreOpened += storeView.OpenTheDoor;
            storeController.OnStoreOpened += HandleStoreOpened;
        }

        private void OnDisable()
        {
            //TODO
        }

        private void HandleStoreOpened()
        {
            viewStateController.SetState(ViewStateEnum.Store);
        }

        public IStreetItemController GetControllerFor(StoreView view)
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
