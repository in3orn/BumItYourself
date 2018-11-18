namespace Krk.Bum.Model.Utils
{
    public class PartLoader
    {
        private const string CountKey = "count";


        private readonly PrefsWrapper wrapper;


        public PartLoader(PrefsWrapper wrapper)
        {
            this.wrapper = wrapper;
        }


        public PartData[] Load(PartConfig[] configs)
        {
            var result = new PartData[configs.Length];

            for(int i = 0; i < configs.Length; i++)
            {
                result[i] = Load(configs[i]);
            }

            return result;
        }

        public PartData Load(PartConfig config)
        {
            return new PartData
            {
                Id = config.Id,
                Name = config.Name,

                Image = config.Image,

                Count = wrapper.GetInt(config.Id, CountKey)
            };
        }


        public void Save(PartData data)
        {
            wrapper.SetInt(data.Id, CountKey, data.Count);
        }
    }
}
