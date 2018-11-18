using Krk.Bum.Model;
using System.Collections.Generic;

namespace Krk.Bum.Game.Core
{
    public class GameStateController
    {
        private List<PartData> loot;


        public List<PartData> Loot { get { return loot; } }


        public GameStateController()
        {
            loot = new List<PartData>();
        }

        public void ClearLoot()
        {
            loot.Clear();
        }

        public void AddLoot(PartData newPart)
        {
            foreach (var part in loot)
            {
                if (part.Id.Equals(newPart.Id))
                {
                    part.Count += newPart.Count;
                    return;
                }
            }

            loot.Add(newPart);
        }
    }
}
