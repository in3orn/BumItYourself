using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Part")]
    public class PartConfig : ScriptableObject
    {
        public string Id;
        public string Name;

        public ImageData Image;
    }
}
