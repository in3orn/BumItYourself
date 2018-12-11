using System;
using System.Collections.Generic;
using Krk.Bum.Model.Utils;
using UnityEngine;
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

        public int ItemsSold
        {
            get { return modelData.ItemsSold; }
        }

        public int ItemsCreated
        {
            get { return modelData.ItemsCreated; }
        }

        public bool IsAnyCollectionSpawned { get; set; }


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
            foreach (var collection in GetAllCollections())
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

        public bool HasPrevItem(string itemId)
        {
            return GetPrevItem(itemId) != null;
        }

        public ItemData GetPrevItem(string itemId)
        {
            foreach (var collection in GetAllCollections())
            {
                for (var i = 0; i < collection.Items.Length; i++)
                {
                    var item = collection.Items[i];
                    if (item.Id.Equals(itemId))
                    {
                        if (i <= 0) return null;
                        return collection.Items[i - 1];
                    }
                }
            }

            return null;
        }

        public bool HasNextItem(string itemId)
        {
            return GetNextItem(itemId) != null;
        }

        public ItemData GetNextItem(string itemId)
        {
            foreach (var collection in GetAllCollections())
            {
                for (var i = 0; i < collection.Items.Length; i++)
                {
                    var item = collection.Items[i];
                    if (item.Id.Equals(itemId))
                    {
                        if (i >= collection.Items.Length - 1) return null;
                        return collection.Items[i + 1];
                    }
                }
            }

            return null;
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

        public void SellAllItems()
        {
            var sold = false;
            foreach (var collection in modelData.Collections)
            {
                if (collection.Unlocked)
                {
                    foreach (var item in collection.Items)
                    {
                        if (CanSellItem(item))
                        {
                            modelData.Cash += item.Reward * item.Count;
                            modelData.ItemsSold += item.Count;

                            item.Count = 0;
                            itemLoader.Save(item);

                            sold = true;
                        }
                    }
                }
            }

            if (sold)
            {
                modelLoader.Save(modelData);

                if (OnItemSold != null) OnItemSold(null);
            }
        }

        public bool CanSellItem(ItemData item)
        {
            return item.Count > 0;
        }

        public void SellItem(ItemData item)
        {
            modelData.Cash += item.Reward;
            modelData.ItemsSold++;
            modelLoader.Save(modelData);

            item.Count--;
            itemLoader.Save(item);

            if (OnItemSold != null) OnItemSold(item);
        }

        public bool CanCreateAnyItem()
        {
            foreach (var collection in modelData.Collections)
            {
                if (collection.Unlocked)
                {
                    foreach (var item in collection.Items)
                    {
                        if (CanCreateItem(item)) return true;
                    }
                }
            }
            return false;
        }

        public bool CanCreateItem(ItemData item)
        {
            if (item.Count >= config.MaxItemCount) return false;

            foreach (var requiredPart in item.RequiredParts)
            {
                var part = GetPart(requiredPart.PartId);
                if (part != null)
                {
                    if (part.Count < requiredPart.RequiredCount) return false;
                }
                else
                {
                    Debug.LogError("Cannot find part: " + requiredPart.PartId);
                }
            }

            return true;
        }

        public void CreateItem(ItemData item)
        {
            item.Count++;
            item.TotalCount++;
            itemLoader.Save(item);

            modelData.ItemsCreated++;
            modelLoader.Save(modelData);

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

        public void CollectPart(PartData collected)
        {
            var part = GetPart(collected.Id);
            var prevCount = part.Count;
            part.Count += collected.Count;
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


        public void CollectAllParts(int value)
        {
            foreach (var part in modelData.Parts)
            {
                part.Count += value;
            }
        }

        public void DecreaseMoney(float price)
        {
            modelData.Cash -= Mathf.RoundToInt(price); //TODO all prices should be float??
            modelLoader.Save(modelData);
        }
    }
}
