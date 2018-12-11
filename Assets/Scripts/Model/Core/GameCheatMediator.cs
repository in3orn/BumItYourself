using Krk.Bum.Model.Context;
using UnityEngine;

namespace Krk.Bum.Model.Core
{
    public class GameCheatMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;

            if (!Application.isEditor) gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                modelController.CollectAllParts(100);
            }
        }
    }
}
