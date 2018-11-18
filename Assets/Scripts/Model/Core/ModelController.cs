using Krk.Bum.Model.Utils;
using System;

namespace Krk.Bum.Model.Core
{
    public class ModelController
    {
        private readonly ModelData modelData;

        private readonly PartLoader partLoader;


        public ModelController(ModelData modelData, PartLoader partLoader)
        {
            this.modelData = modelData;
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
