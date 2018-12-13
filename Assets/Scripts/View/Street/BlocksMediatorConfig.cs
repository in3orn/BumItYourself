using UnityEngine;

namespace Krk.Bum.View.Street
{
    [CreateAssetMenu(menuName = "Krk/View/Street/Blocks Mediator")]
    public class BlocksMediatorConfig : ScriptableObject
    {
        public BlockView FirstTemplate;

        public BlockView[] Templates;

        public BlockView[] TestTemplates;
    }
}
