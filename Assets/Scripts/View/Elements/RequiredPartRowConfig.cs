using UnityEngine;

namespace Krk.Bum.View.Elements
{
    [CreateAssetMenu(menuName = "Krk/View/Elements/Required Part Row")]
    public class RequiredPartRowConfig : ScriptableObject
    {
        public string CountFormat;
        public Color AvailableColor;
        public Color UnavailableColor;
    }
}
