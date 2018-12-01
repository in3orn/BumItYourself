using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class ResourcesMediator : MonoBehaviour
    {
        [SerializeField]
        private ResourcesDisplay display = null;

        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }

        private void Start()
        {
            display.InitValue(modelController.GetResourcesCount());
        }

        private void OnEnable()
        {
            modelController.OnPartCollected += HandlePartCollected;
            modelController.OnItemCreated += HandleItemCreated;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                modelController.OnPartCollected -= HandlePartCollected;
                modelController.OnItemCreated -= HandleItemCreated;
            }
        }

        private void HandlePartCollected(PartData data)
        {
            display.UpdateValue(modelController.GetResourcesCount());
        }

        private void HandleItemCreated(ItemData data)
        {
            display.UpdateValue(modelController.GetResourcesCount());
        }
    }
}
