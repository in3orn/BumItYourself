using UnityEngine;

namespace Krk.Bum.View.Sounds
{
    [CreateAssetMenu(menuName = "Krk/View/Sounds/Sound Player")]
    public class SoundPlayerConfig : ScriptableObject
    {
        public AudioClip[] TrashHits = null;
        public AudioClip[] CraftItem = null;
        public AudioClip[] SellItem = null;
        public AudioClip[] UnlockCollection = null;
        public AudioClip[] CollectPart = null;
    }
}
