using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class ItemsButtonMediator : MonoBehaviour
    {
        [SerializeField]
        private StreetButtonView button = null;

        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }


        private void Start()
        {
            if (modelController.IsAnyCollectionUnlocked()) button.Init();
        }


        private void OnEnable()
        {
            modelController.OnCollectionUnlocked += HandleCollectionUnlocked;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                modelController.OnCollectionUnlocked += HandleCollectionUnlocked;
            }
        }

        private void HandleCollectionUnlocked(CollectionData collection)
        {
            if (!button.Shown) button.Show();
        }
    }
}
