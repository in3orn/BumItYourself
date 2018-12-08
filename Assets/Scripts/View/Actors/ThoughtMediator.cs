using System;
using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using Krk.Bum.View.Model;
using Krk.Bum.View.Street;
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

        
        private ThoughtsProvider startThoughtsProvider;

        private ThoughtsProvider collectionThoughtsProvider;

        private ViewStateController viewStateController;


        private ModelController modelController;

        private CollectionController collectionController;


        private bool collectionSpawned;


        private void Awake()
        {
            startThoughtsProvider = viewContext.StartThoughtsProvider;
            collectionThoughtsProvider = viewContext.CollectionThoughtsProvider;
            viewStateController = viewContext.ViewStateController;

            modelController = modelContext.ModelController;
            collectionController = modelContext.CollectionController;
        }

        private void OnEnable()
        {
            thoughtView.OnThoughtEnded += HandleThoughtEnded;

            viewStateController.OnStateChanged += HandleViewStateChanged;
            collectionController.OnSpawned += HandleCollectionSpawned;
        }

        private void OnDisable()
        {
            if (thoughtView != null)
            {
                thoughtView.OnThoughtEnded -= HandleThoughtEnded;
            }

            if (modelContext != null)
            {
                collectionController.OnSpawned -= HandleCollectionSpawned;
            }
        }
        
        private void HandleThoughtEnded()
        {
            TryShowStartThought();
            TryShowCollectionThought();
        }
        

        private void HandleViewStateChanged(ViewStateEnum state)
        {
            if (state == ViewStateEnum.Street)
            {
                TryShowStartThought();
            }
        }

        private void TryShowStartThought()
        {
            if (!modelController.IsAnyCollectionUnlocked())
            {
                var thought = startThoughtsProvider.GetNextThought();
                if (thought != null) thoughtView.Show(thought);
            }
        }


        private void HandleCollectionSpawned()
        {
            collectionSpawned = true;
            TryShowCollectionThought();
        }

        private void TryShowCollectionThought()
        {
            if (collectionSpawned && !modelController.IsAnyCollectionUnlocked())
            {
                var thought = collectionThoughtsProvider.GetNextThought();
                if (thought != null) thoughtView.Show(thought);
            }
        }
    }
}
