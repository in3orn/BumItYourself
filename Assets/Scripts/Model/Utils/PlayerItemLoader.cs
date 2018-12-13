using System;

namespace Krk.Bum.Model.Utils
{
    public class PlayerItemLoader
    {
        private const string UnlockedKey = "unlocked";

        
        private readonly PrefsWrapper wrapper;


        public PlayerItemLoader(PrefsWrapper wrapper)
        {
            this.wrapper = wrapper;
        }


        public PlayerItemData[] Load(PlayerItemConfig[] configs)
        {
            var result = new PlayerItemData[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                result[i] = Load(configs[i]);
            }

            return result;
        }

        public PlayerItemData Load(PlayerItemConfig config)
        {
            return new PlayerItemData
            {
                Id = config.Id,
                Name = config.Name,
                Price = config.Price,

                Image = config.Image,

                Unlocked = config.Price <= 0 || wrapper.GetBool(config.Id, UnlockedKey)
            };
        }


        public void Save(PlayerItemData data)
        {
            wrapper.SetBool(data.Id, UnlockedKey, data.Unlocked);
        }

        internal PlayerItemData[] Load(object beards)
        {
            throw new NotImplementedException();
        }
    }
}
