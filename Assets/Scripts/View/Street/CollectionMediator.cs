using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class CollectionMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;


        private CollectionController collectionController;

        private PartView partView; //TODO collectionView


        private void Awake()
        {
            collectionController = modelContext.CollectionController;
        }

        public CollectionController GetControllerFor(PartView part)
        {
            if (!part.Data.IsCollection) return null;

            partView = part;

            collectionController.Position = part.transform.position;
            return collectionController;
        }

        private void OnEnable()
        {
            collectionController.OnUsed += HandleCollectionUsed;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                collectionController.OnUsed -= HandleCollectionUsed;
            }
        }

        private void HandleCollectionUsed()
        {
            partView.OnCollected += HandlePartCollected;
            partView.Collect();
        }

        private void HandlePartCollected(PartView partView)
        {
            partView.OnCollected -= HandlePartCollected;
            
            HandleCollectionCollected(partView.Data);
        }

        public void HandleCollectionCollected(PartData partData)
        {
            collectionController.Collect(partData.Id);
        }
    }
}
