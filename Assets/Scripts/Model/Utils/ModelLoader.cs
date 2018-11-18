namespace Krk.Bum.Model.Utils
{
    public class ModelLoader
    {
        private readonly CollectionLoader collectionLoader;

        private readonly PartLoader partLoader;


        public ModelLoader(CollectionLoader collectionLoader, PartLoader partLoader)
        {
            this.collectionLoader = collectionLoader;
            this.partLoader = partLoader;
        }


        public ModelData Load(ModelConfig config)
        {
            return new ModelData
            {
                Collections = collectionLoader.Load(config.Collections),
                Parts = partLoader.Load(config.Parts)
            };
        }
    }
}
