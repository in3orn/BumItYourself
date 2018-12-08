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



        private void Awake()
        {
            modelController = modelContext.ModelController;
            startThoughtsProvider = viewContext.StartThoughtsProvider;
        }

        private void Start()
        {
            TryShowStartThought();
        }

        private void OnEnable()
        {
            thoughtView.OnThoughtEnded += HandleThoughtEnded;
        }

        private void OnDisable()
        {
            thoughtView.OnThoughtEnded -= HandleThoughtEnded;
        }

        private void HandleThoughtEnded()
        {
            TryShowStartThought();
        }

        private void TryShowStartThought()
        {
            if (!modelController.IsAnyCollectionUnlocked())
            {
                var thought = startThoughtsProvider.GetNextThought();
                if (thought != null) thoughtView.Show(thought);
            }
        }
    }
}
