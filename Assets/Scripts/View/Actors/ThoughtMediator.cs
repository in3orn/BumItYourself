using System;
using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class ThoughtMediator : MonoBehaviour
    {
        [SerializeField]
        private ThoughtView thoughtView = null;

        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ViewContext viewContext = null;


        private ModelController modelController;

        private ThoughtsProvider startThoughtsProvider;

        private ThoughtsProvider collectionThoughtsProvider;


        private string collectionId;


        private void Awake()
        {
            modelController = modelContext.ModelController;
            startThoughtsProvider = viewContext.StartThoughtsProvider;
            collectionThoughtsProvider = viewContext.CollectionThoughtsProvider;
        }

        private void Start()
        {
            TryShowStartThought();
        }

        private void OnEnable()
        {
            thoughtView.OnThoughtEnded += HandleThoughtEnded;

            modelController.OnCollectionUnlocked += HandleCollectionUnlocked;
        }

        private void OnDisable()
        {
            if (thoughtView != null)
            {
                thoughtView.OnThoughtEnded -= HandleThoughtEnded;
            }

            if (modelContext != null)
            {
                modelController.OnCollectionUnlocked -= HandleCollectionUnlocked;
            }
        }

        private void HandleThoughtEnded()
        {
            TryShowStartThought();
            TryShowCollectionthought();
        }

        private void TryShowStartThought()
        {
            if (!modelController.IsAnyCollectionUnlocked())
            {
                var thought = startThoughtsProvider.GetNextThought();
                if (thought != null) thoughtView.Show(thought);
            }
        }

        private void HandleCollectionUnlocked(CollectionData collectionData)
        {
            collectionId = collectionData.Id;
            TryShowCollectionthought();
        }

        private void TryShowCollectionthought()
        {
            if (collectionId == modelController.GetAllCollections()[0].Id)
            {
                var thought = collectionThoughtsProvider.GetNextThought();
                if (thought != null) thoughtView.Show(thought);
            }
        }
    }
}
