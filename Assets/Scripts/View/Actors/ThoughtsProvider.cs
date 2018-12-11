namespace Krk.Bum.View.Actors
{
    public class ThoughtsProvider
    {
        private readonly ThoughtsProviderConfig config;
        

        private int index;


        public ThoughtsProvider(ThoughtsProviderConfig config)
        {
            this.config = config;
        }


        public ThoughtData GetNextThought()
        {
            if(index < config.Thoughts.Length)
            {
                return config.Thoughts[index++];
            }

            return null;
        }
    }
}
