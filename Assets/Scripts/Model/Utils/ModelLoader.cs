namespace Krk.Bum.Model.Utils
{
    public class ModelLoader
    {
        private readonly CollectionLoader collectionLoader;


        public ModelLoader(CollectionLoader collectionLoader)
        {
            this.collectionLoader = collectionLoader;
        }


        public ModelData Load(ModelConfig config)
        {
            return new ModelData
            {
                Collections = collectionLoader.Load(config.Collections)
            };
        }
    }
}
