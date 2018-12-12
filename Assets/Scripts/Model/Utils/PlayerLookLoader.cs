namespace Krk.Bum.Model.Utils
{
    public class PlayerLookLoader
    {
        private const string CurrentBodyKey= "current-body";
        private const string CurrentBagKey = "current-bag";


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

                Bodies = playerItemLoader.Load(config.Bodies),
                Bags = playerItemLoader.Load(config.Bags)
            };
        }

        public void Save(PlayerLookData data)
        {
            wrapper.SetString(CurrentBodyKey, data.CurrentBodyId);
            wrapper.SetString(CurrentBagKey, data.CurrentBagId);
        }
    }
}
