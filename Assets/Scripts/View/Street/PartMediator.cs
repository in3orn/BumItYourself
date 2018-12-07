using Krk.Bum.Model;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class PartMediator : MonoBehaviour
    {
        [SerializeField]
        private PartMediatorConfig config = null;

        [SerializeField]
        private RectTransform inventoryButton = null;


        public void Spawn(TrashView trash, PartData partData)
        {
            var gameObject = Instantiate(config.Template, transform);
            gameObject.transform.position = trash.transform.position;

            var partView = gameObject.GetComponent<PartView>();
            partView.TargetTransform = inventoryButton;
            partView.DrawOrder = trash.DrawOrder + 9;
            partView.Show(partData);
        }
    }
}
