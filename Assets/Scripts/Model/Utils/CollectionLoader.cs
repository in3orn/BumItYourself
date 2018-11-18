using System;

namespace Krk.Bum.Model.Utils
{
    public class CollectionLoader
    {
        private const string UnlockedKey = "unlocked";


        private readonly ItemLoader itemLoader;
        private readonly PrefsWrapper wrapper;


        public CollectionLoader(ItemLoader itemLoader, PrefsWrapper wrapper)
        {
            this.itemLoader = itemLoader;
            this.wrapper = wrapper;
        }


        public CollectionData[] Load(CollectionConfig[] configs)
        {
            var result = new CollectionData[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                result[i] = Load(configs[i]);
            }

            return result;
        }

        public CollectionData Load(CollectionConfig config)
        {
            return new CollectionData
            {
                Id = config.Id,
                Name = config.Name,

                Image = config.Image,

                Unlocked = wrapper.GetBool(config.Id, UnlockedKey),

                Items = itemLoader.Load(config.Items)
            };
        }


        public void Save(CollectionData data)
        {
            wrapper.SetBool(data.Id, UnlockedKey, data.Unlocked);
        }
    }
}
