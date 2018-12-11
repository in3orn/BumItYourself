using UnityEngine;

namespace Krk.Bum.View.Actors
{
    [CreateAssetMenu(menuName = "Krk/View/Actors/Thoughts Provider")]
    public class ThoughtsProviderConfig : ScriptableObject
    {
        public ThoughtData[] Thoughts;
    }
}
