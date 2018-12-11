using UnityEngine;

namespace Krk.Bum.View.Screens
{
    [CreateAssetMenu(menuName = "Krk/View/Screens/Intro Screen")]
    public class IntroScreenConfig : ScriptableObject
    {
        public SubtitleData[] Subtitles;

        public float FadeInDuration;
        public float FadeOutDuration;
    }
}
