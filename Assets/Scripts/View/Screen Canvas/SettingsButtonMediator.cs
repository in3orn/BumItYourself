using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class SettingsButtonMediator : MonoBehaviour
    {
        [SerializeField]
        private StreetButtonView button = null;


        private void Start()
        {
            button.Init();
        }
    }
}
