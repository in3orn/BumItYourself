namespace Krk.Bum.Model.Utils
{
    public class ItemLoader
    {
        private const string CountKey = "count";


        private readonly PrefsWrapper wrapper;


        public ItemLoader(PrefsWrapper wrapper)
        {
            this.wrapper = wrapper;
        }


        public ItemData[] Load(ItemConfig[] configs)
        {
            var result = new ItemData[configs.Length];

            for(int i = 0; i < configs.Length; i++)
            {
                result[i] = Load(configs[i]);
            }

            return result;
        }

        public ItemData Load(ItemConfig config)
        {
            return new ItemData
            {
                Id = config.Id,
                Name = config.Name,

                Image = config.Image,

                Count = wrapper.GetInt(config.Id, CountKey),

                RequiredParts = config.RequiredParts
            };
        }


        public void Save(ItemData data)
        {
            wrapper.SetInt(data.Id, CountKey, data.Count);
        }
    }
}
