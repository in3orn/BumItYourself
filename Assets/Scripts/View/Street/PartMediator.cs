using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class PartMediator : MonoBehaviour
    {
        [SerializeField]
        private PartMediatorConfig config = null;

        [SerializeField]
        private RectTransform inventoryButton = null;

        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private Dictionary<PartView, PartData> parts;


        public PartMediator()
        {
            parts = new Dictionary<PartView, PartData>();
        }


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }


        public void Spawn(TrashView trash, PartData partData)
        {
            var gameObject = Instantiate(config.Template, transform);
            gameObject.transform.position = trash.transform.position;

            var partView = gameObject.GetComponent<PartView>();
            partView.TargetTransform = inventoryButton;
            partView.DrawOrder = trash.DrawOrder + 9;
            partView.Spawn(partData);

            partView.OnCollected += HandlePartCollected;

            parts[partView] = partData;
        }

        private void HandlePartCollected(PartView partView)
        {
            var partData = parts[partView];
            if (!partData.IsCollection)
            {
                modelController.CollectPart(partData);
            }

            partView.OnCollected -= HandlePartCollected;
            parts.Remove(partView);
        }
    }
}
