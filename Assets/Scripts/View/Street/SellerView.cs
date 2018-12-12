using Krk.Bum.Game.Items;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class SellerView : MonoBehaviour
    {
        [SerializeField]
        private SellerControllerConfig sellerControllerConfig = null;


        public SellerControllerConfig SellerControllerConfig { get { return sellerControllerConfig; } }


        public void OpenTheDoor(SellerControllerConfig sellerControllerConfig)
        {
        }

        public void CloseTheDoor()
        {
        }
    }
}
