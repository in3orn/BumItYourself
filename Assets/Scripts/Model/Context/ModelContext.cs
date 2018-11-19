using Krk.Bum.Model.Core;
using Krk.Bum.Model.Utils;
using UnityEngine;

namespace Krk.Bum.Model.Context
{
    public class ModelContext : MonoBehaviour
    {
        public ModelConfig ModelConfig;


        private ModelController modelController;

        public ModelController ModelController
        {
            get
            {
                return modelController ?? (modelController =
                  new ModelController(ModelData, ItemLoader, PartLoader));
            }
        }

        private ModelData modelData;

        public ModelData ModelData
        {
            get { return modelData ?? (modelData = ModelLoader.Load(ModelConfig)); }
        }

        private ModelLoader modelLoader;

        public ModelLoader ModelLoader
        {
            get { return modelLoader ?? (modelLoader = new ModelLoader(CollectionLoader, PartLoader)); }
        }

        private CollectionLoader collectionLoader;

        public CollectionLoader CollectionLoader
        {
            get { return collectionLoader ?? (collectionLoader = new CollectionLoader(ItemLoader, PrefsWrapper)); }
        }

        private ItemLoader itemLoader;

        public ItemLoader ItemLoader
        {
            get { return itemLoader ?? (itemLoader = new ItemLoader(PrefsWrapper)); }
        }

        private PartLoader partLoader;

        public PartLoader PartLoader
        {
            get { return partLoader ?? (partLoader = new PartLoader(PrefsWrapper)); }
        }

        private PrefsWrapper prefsWrapper;

        public PrefsWrapper PrefsWrapper
        {
            get { return prefsWrapper ?? (prefsWrapper = new PrefsWrapper()); }
        }
    }
}
