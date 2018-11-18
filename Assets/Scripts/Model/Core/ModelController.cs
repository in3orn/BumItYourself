namespace Krk.Bum.Model.Core
{
    public class ModelController
    {
        private readonly ModelData modelData;


        public ModelController(ModelData modelData)
        {
            this.modelData = modelData;
        }


        public CollectionData[] GetAllCollections()
        {
            return modelData.Collections;
        }

        public CollectionData GetCollection(string id)
        {
            foreach(var collection in modelData.Collections)
            {
                if (collection.Id.Equals(id)) return collection;
            }

            return null;
        }
    }
}
