using UnityEngine;
using Krk.Bum.Common;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.Model;

namespace Krk.Bum.View.Screens
{
    public class ItemScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private ItemScreenView screenView = null;


        private ModelController modelController;

        private IButtonListener backListener;


        protected override void Awake()
        {
            base.Awake();
            modelController = modelContext.ModelController;
            backListener = viewContext.BackButtonListener;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                var collectionId = viewStateController.CurrentCollectionId;
                var itemId = viewStateController.CurrentItemId;
                var item = modelController.GetItem(collectionId, itemId);

                screenView.Init(item, GetRequiredParts(item));
            }
            base.SetShown(shown);
        }

        private RequiredPartData[] GetRequiredParts(ItemData item)
        {
            var result = new RequiredPartData[item.RequiredParts.Length];

            for (int i = 0; i < result.Length; i++)
            {
                var itemPart = item.RequiredParts[i];
                var part = modelController.GetPart(itemPart.PartId);
                result[i] = new RequiredPartData
                {
                    Id = part.Id,
                    Name = part.Name,
                    Image = part.Image,
                    Count = part.Count,
                    RequiredCount = itemPart.RequiredCount
                };
            }

            return result;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            backListener.Subscribe(screenView.BackButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (viewContext != null && screenView != null)
            {
                backListener.Unsubscribe(screenView.BackButton);
            }
        }
    }
}
