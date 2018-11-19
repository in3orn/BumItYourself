using Krk.Bum.Model.Utils;

namespace Krk.Bum.Model.Core
{
    public class ModelController
    {
        private readonly ModelData modelData;

        private readonly ItemLoader itemLoader;

        private readonly PartLoader partLoader;


        public ModelController(ModelData modelData, ItemLoader itemLoader, PartLoader partLoader)
        {
            this.modelData = modelData;
            this.itemLoader = itemLoader;
            this.partLoader = partLoader;
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

        public ItemData GetItem(string collectionId, string itemId)
        {
            var collection = GetCollection(collectionId);

            foreach (var item in collection.Items)
            {
                if (item.Id.Equals(itemId)) return item;
            }

            return null;
        }

        public bool CanCreateItem(ItemData item)
        {
            if (item.Count >= 99) return false; //TODO max count in model config?

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

        public void IncreasePartCount(string id, int value)
        {
            var part = GetPart(id);
            part.Count += value;
            partLoader.Save(part);
        }
    }
}
