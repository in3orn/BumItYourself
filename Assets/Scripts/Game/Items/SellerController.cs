using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.Game.Items
{
    public class SellerController : IStreetItemController
    {
        public UnityAction<SellerControllerConfig> OnStoreOpened;


        private readonly SellerControllerConfig config;


        public Vector2 Position { get; set; }


        public SellerController(SellerControllerConfig config)
        {
            this.config = config;
        }


        public void Use()
        {
            if (OnStoreOpened != null) OnStoreOpened(config);
        }
    }
}
