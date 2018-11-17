using Krk.Bum.View.Model;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    [CreateAssetMenu(menuName = "Krk/View/Screens/Screen Mediator")]
    public class ScreenMediatorConfig : ScriptableObject
    {
        public ViewStateEnum[] ShowScreenStates;
    }
}
