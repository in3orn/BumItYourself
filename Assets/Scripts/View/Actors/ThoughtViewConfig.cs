using UnityEngine;

namespace Krk.Bum.View.Actors
{
    [CreateAssetMenu(menuName = "Krk/View/Actors/Thought View")]
    public class ThoughtViewConfig : ScriptableObject
    {
        public float FadeInDuration;
        public float FadeOutDuration;
    }
}
