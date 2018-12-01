using UnityEngine;

namespace Krk.Bum.Model.Core
{
    [CreateAssetMenu(menuName = "Krk/Model/Core/Model Controller")]
    public class ModelControllerConfig : ScriptableObject
    {
        public int MaxItemCount;
        public int MaxResourceCount;
    }
}
