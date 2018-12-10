using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Context;
using UnityEngine;

namespace Krk.Bum.View.Model
{
    public class ViewStateMediator : MonoBehaviour
    {
        [SerializeField]
        private ViewContext viewContext = null;

        [SerializeField]
        private ModelContext modelContext = null;

        
        private ViewStateController viewStateController;

        private ModelController modelController;


        private void Awake()
        {
            viewStateController = viewContext.ViewStateController;
            modelController = modelContext.ModelController;

            if (modelController.IsAnyCollectionUnlocked())
            {
                viewStateController.InitState(ViewStateEnum.Street);
            }
            else
            {
                viewStateController.InitState(ViewStateEnum.Intro);
            }
        }
    }
}
