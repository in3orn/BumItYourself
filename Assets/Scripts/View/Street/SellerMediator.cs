using System.Collections.Generic;
using Krk.Bum.Game.Items;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class SellerMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ViewContext viewContext = null;


        private ModelController modelController;
        private BlocksController blocksController;
        private ViewStateController viewStateController;


        private Dictionary<SellerView, IStreetItemController> stores;


        private void Awake()
        {
            stores = new Dictionary<SellerView, IStreetItemController>();

            modelController = modelContext.ModelController;
            blocksController = viewContext.BlocksController;
            viewStateController = viewContext.ViewStateController;
        }

        private void OnEnable()
        {
            blocksController.OnBlockSpawned += HandleBlockSpawned;
        }

        private void HandleBlockSpawned(BlockView blockView, BlockData data)
        {
            var sellerView = blockView.GetComponentInChildren<SellerView>();
            if (sellerView != null)
            {
                var sellerController = new SellerController()
                {
                    Position = sellerView.transform.position
                };

                stores[sellerView] = sellerController;

                sellerController.OnStoreOpened += sellerView.OpenTheDoor;
                sellerController.OnStoreOpened += HandleStoreOpened;
            }
        }

        private void OnDisable()
        {
            //TODO
        }

        private void HandleStoreOpened()
        {
            viewStateController.SetState(ViewStateEnum.Seller);
        }

        public IStreetItemController GetControllerFor(SellerView view)
        {
            if (stores.ContainsKey(view))
            {
                var result = stores[view];
                result.Position = view.transform.position; //TODO hacky :(

                return result;
            }
            return null;
        }
    }
}
