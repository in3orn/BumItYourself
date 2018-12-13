namespace Krk.Bum.Model.Utils
{
    public class PlayerLookLoader
    {
        private const string CurrentBodyKey= "current-body";
        private const string CurrentBagKey = "current-bag";
        private const string CurrentStickKey = "current-stick";


        private readonly PlayerItemLoader playerItemLoader;

        private readonly PrefsWrapper wrapper;



        public PlayerLookLoader(PlayerItemLoader playerBodyLoader, PrefsWrapper wrapper)
        {
            this.playerItemLoader = playerBodyLoader;
            this.wrapper = wrapper;
        }


        public PlayerLookData Load(PlayerLookConfig config)
        {
            return new PlayerLookData
            {
                CurrentBodyId = wrapper.GetString(CurrentBodyKey),
                CurrentBagId = wrapper.GetString(CurrentBagKey),
                CurrentStickId = wrapper.GetString(CurrentStickKey),

                Bodies = playerItemLoader.Load(config.Bodies),
                Bags = playerItemLoader.Load(config.Bags),
                Sticks = playerItemLoader.Load(config.Sticks)
            };
        }

        public void Save(PlayerLookData data)
        {
            wrapper.SetString(CurrentBodyKey, data.CurrentBodyId);
            wrapper.SetString(CurrentBagKey, data.CurrentBagId);
            wrapper.SetString(CurrentStickKey, data.CurrentStickId);
        }
    }
}
