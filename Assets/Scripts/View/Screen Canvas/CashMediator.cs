using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class CashMediator : MonoBehaviour
    {
        [SerializeField]
        private CountDisplay display = null;

        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }

        private void Start()
        {
            if(modelController.ItemsSold > 0)
                display.Init(modelController.Cash);
        }

        private void OnEnable()
        {
            modelController.OnItemSold += HandleItemSold;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                modelController.OnItemSold -= HandleItemSold;
            }
        }

        private void HandleItemSold(ItemData data)
        {
            if (display.Shown)
                display.IncreaseValue(modelController.Cash);
            else
                display.Show(modelController.Cash);
        }
    }
}
