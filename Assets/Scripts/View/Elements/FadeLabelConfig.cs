using Krk.Bum.View.Animations;
using UnityEngine;

namespace Krk.Bum.View.Elements
{
    [CreateAssetMenu(menuName = "Krk/View/Elements/Fade Label")]
    public class FadeLabelConfig : ScriptableObject
    {
        public FadeAnimationConfig Fade;

        public MoveAnimationConfig Move;
    }
}
