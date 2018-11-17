using Krk.Bum.View.Core;
using UnityEngine;

namespace Krk.Bum.View.Context
{
    public class ViewContext : MonoBehaviour
    {
        private ViewStateController viewStateController;

        public ViewStateController ViewStateController
        {
            get
            {
                if (viewStateController == null)
                {
                    viewStateController = new ViewStateController();
                }
                return viewStateController;
            }
        }
    }
}
