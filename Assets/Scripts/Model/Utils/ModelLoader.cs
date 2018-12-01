namespace Krk.Bum.Model.Utils
{
    public class ModelLoader
    {
        private const string CashKey = "cash";
        private const string ItemsSoldKey = "items-sold";


        private readonly PrefsWrapper wrapper;

        private readonly CollectionLoader collectionLoader;

        private readonly PartLoader partLoader;


        public ModelLoader(PrefsWrapper wrapper, CollectionLoader collectionLoader, PartLoader partLoader)
        {
            this.wrapper = wrapper;
            this.collectionLoader = collectionLoader;
            this.partLoader = partLoader;
        }


        public ModelData Load(ModelConfig config)
        {
            return new ModelData
            {
                Collections = collectionLoader.Load(config.Collections),
                Parts = partLoader.Load(config.Parts),
                Cash = wrapper.GetInt(CashKey),
                ItemsSold = wrapper.GetInt(ItemsSoldKey)
            };
        }

        public void Save(ModelData data)
        {
            wrapper.SetInt(CashKey, data.Cash);
            wrapper.SetInt(ItemsSoldKey, data.ItemsSold);
        }
    }
}
