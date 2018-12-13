using Krk.Bum.Model.Core;
using Krk.Bum.Model.Utils;
using Krk.Bum.View.Street;
using UnityEngine;

namespace Krk.Bum.Model.Context
{
    public class ModelContext : MonoBehaviour
    {
        public ModelControllerConfig ModelControllerConfig;

        private ModelController modelController;

        public ModelController ModelController
        {
            get
            {
                return modelController ?? (modelController =
                  new ModelController(ModelControllerConfig, ModelData,
                  ModelLoader, CollectionLoader, ItemLoader, PartLoader));
            }
        }

        public ModelConfig ModelConfig;

        private ModelData modelData;

        public ModelData ModelData
        {
            get { return modelData ?? (modelData = ModelLoader.Load(ModelConfig)); }
        }

        private ModelLoader modelLoader;

        public ModelLoader ModelLoader
        {
            get { return modelLoader ?? (modelLoader = new ModelLoader(PrefsWrapper, CollectionLoader, PartLoader)); }
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


        private CollectionController collectionController;

        public CollectionController CollectionController
        {
            get
            {
                return collectionController ?? (collectionController = new CollectionController(ModelController));
            }
        }


        public PlayerLookConfig PlayerLookConfig;

        private PlayerLookData playerLookData;

        public PlayerLookData PlayerLookData
        {
            get { return playerLookData ?? (playerLookData = PlayerLookLoader.Load(PlayerLookConfig)); }
        }

        private PlayerLookController playerLookController;

        public PlayerLookController PlayerLookController
        {
            get
            {
                return playerLookController ?? (playerLookController =
                    new PlayerLookController(PlayerLookData, PlayerLookLoader, PlayerBodyLoader));
            }
        }

        private PlayerLookLoader playerLookLoader;

        public PlayerLookLoader PlayerLookLoader
        {
            get
            {
                return playerLookLoader ?? (playerLookLoader = 
                    new PlayerLookLoader(PlayerBodyLoader, PrefsWrapper));
            }
        }

        private PlayerItemLoader playerBodyLoader;

        public PlayerItemLoader PlayerBodyLoader
        {
            get
            {
                return playerBodyLoader ?? (playerBodyLoader = new PlayerItemLoader(PrefsWrapper));
            }
        }
    }
}
