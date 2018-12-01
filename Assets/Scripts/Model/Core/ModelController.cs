using System.Collections.Generic;
using Krk.Bum.Model.Utils;
using UnityEngine.Events;

namespace Krk.Bum.Model.Core
{
    public class ModelController
    {
        public UnityAction<CollectionData> OnCollectionUnlocked;
        public UnityAction<PartData> OnPartCollected;
        public UnityAction<ItemData> OnItemCreated;
        public UnityAction<ItemData> OnItemSold;


        private readonly ModelControllerConfig config;

        private readonly ModelData modelData;

        private readonly CollectionLoader collectionLoader;

        private readonly ModelLoader modelLoader;

        private readonly ItemLoader itemLoader;

        private readonly PartLoader partLoader;


        public int Cash
        {
            get { return modelData.Cash; }
        }


        public ModelController(ModelControllerConfig config, ModelData modelData,
            ModelLoader modelLoader, CollectionLoader collectionLoader, 
            ItemLoader itemLoader, PartLoader partLoader)
        {
            this.config = config;
            this.modelData = modelData;
            this.collectionLoader = collectionLoader;
            this.modelLoader = modelLoader;
            this.itemLoader = itemLoader;
            this.partLoader = partLoader;
        }

        public bool IsAnyCollectionUnlocked()
        {
            foreach(var collection in GetAllCollections())
            {
                if (collection.Unlocked) return true;
            }

            return false;
        }

        public CollectionData[] GetAllCollections()
        {
            return modelData.Collections;
        }

        public CollectionData GetCollection(string id)
        {
            foreach (var collection in modelData.Collections)
            {
                if (collection.Id.Equals(id)) return collection;
            }

            return null;
        }

        public void UnlockCollection(CollectionData collection)
        {
            collection.Unlocked = true;
            collectionLoader.Save(collection);
            if (OnCollectionUnlocked != null) OnCollectionUnlocked(collection);
        }

        public List<ItemData> GetCreatedItems()
        {
            var result = new List<ItemData>();

            foreach (var collection in modelData.Collections)
            {
                foreach (var item in collection.Items)
                {
                    if (item.Count > 0) result.Add(item);
                }
            }

            return result;
        }

        public ItemData GetItem(string collectionId, string itemId)
        {
            var collection = GetCollection(collectionId);

            foreach (var item in collection.Items)
            {
                if (item.Id.Equals(itemId)) return item;
            }

            return null;
        }

        public ItemData GetItem(string itemId)
        {
            foreach (var collection in GetAllCollections())
            {
                foreach (var item in collection.Items)
                {
                    if (item.Id.Equals(itemId)) return item;
                }
            }

            return null;
        }

        public bool CanSellItem(ItemData item)
        {
            return item.Count > 0;
        }

        public void SellItem(ItemData item)
        {
            modelData.Cash += item.Reward;
            modelLoader.Save(modelData);

            item.Count--;
            itemLoader.Save(item);

            if (OnItemSold != null) OnItemSold(item);
        }

        public bool CanCreateItem(ItemData item)
        {
            if (item.Count >= config.MaxItemCount) return false;

            foreach (var requiredPart in item.RequiredParts)
            {
                var part = GetPart(requiredPart.PartId);
                if (part.Count < requiredPart.RequiredCount) return false;
            }

            return true;
        }

        public void CreateItem(ItemData item)
        {
            item.Count++;
            itemLoader.Save(item);

            foreach (var requiredPart in item.RequiredParts)
            {
                var part = GetPart(requiredPart.PartId);
                part.Count -= requiredPart.RequiredCount;
                partLoader.Save(part);
            }

            if (OnItemCreated != null) OnItemCreated(item);
        }

        public RequiredPartData[] GetRequiredParts(ItemData item)
        {
            var result = new RequiredPartData[item.RequiredParts.Length];

            for (int i = 0; i < result.Length; i++)
            {
                var itemPart = item.RequiredParts[i];
                var part = GetPart(itemPart.PartId);
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

        public PartData[] GetAllParts()
        {
            return modelData.Parts;
        }

        public PartData GetPart(string id)
        {
            foreach (var part in modelData.Parts)
            {
                if (part.Id.Equals(id)) return part;
            }
            return null;
        }

        public bool CanCollectPart(PartData part)
        {
            return part.Count < config.MaxResourceCount;
        }

        public void CollectPart(PartData part, int value)
        {
            var prevCount = part.Count;
            part.Count += value;
            if (part.Count > config.MaxResourceCount) part.Count = config.MaxResourceCount;

            if (part.Count != prevCount)
            {
                partLoader.Save(part);

                if (OnPartCollected != null) OnPartCollected(part);
            }
        }

        public int GetResourcesCount()
        {
            var count = 0;
            foreach (var part in modelData.Parts)
            {
                count += part.Count;
            }
            return count;
        }
    }
}
