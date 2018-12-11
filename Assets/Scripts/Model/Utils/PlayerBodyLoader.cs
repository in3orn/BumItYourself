namespace Krk.Bum.Model.Utils
{
    public class PlayerBodyLoader
    {
        private const string UnlockedKey = "unlocked";

        
        private readonly PrefsWrapper wrapper;


        public PlayerBodyLoader(PrefsWrapper wrapper)
        {
            this.wrapper = wrapper;
        }


        public PlayerBodyData[] Load(PlayerBodyConfig[] configs)
        {
            var result = new PlayerBodyData[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                result[i] = Load(configs[i]);
            }

            return result;
        }

        public PlayerBodyData Load(PlayerBodyConfig config)
        {
            return new PlayerBodyData
            {
                Id = config.Id,
                Name = config.Name,
                Price = config.Price,

                Image = config.Image,

                Unlocked = config.Price <= 0 || wrapper.GetBool(config.Id, UnlockedKey)
            };
        }


        public void Save(CollectionData data)
        {
            wrapper.SetBool(data.Id, UnlockedKey, data.Unlocked);
        }
    }
}
