namespace Krk.Bum.Model.Utils
{
    public class PlayerLookLoader
    {
        private const string CurrentBodyKey= "current-body";


        private readonly PlayerBodyLoader playerBodyLoader;

        private readonly PrefsWrapper wrapper;



        public PlayerLookLoader(PlayerBodyLoader playerBodyLoader, PrefsWrapper wrapper)
        {
            this.playerBodyLoader = playerBodyLoader;
            this.wrapper = wrapper;
        }


        public PlayerLookData Load(PlayerLookConfig config)
        {
            return new PlayerLookData
            {
                CurrentBodyId = wrapper.GetString(CurrentBodyKey),

                Bodies = playerBodyLoader.Load(config.Bodies)
            };
        }

        public void Save(PlayerLookData data)
        {
            wrapper.SetString(CurrentBodyKey, data.CurrentBodyId);
        }
    }
}
