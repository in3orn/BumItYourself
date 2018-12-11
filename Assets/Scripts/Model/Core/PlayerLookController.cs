using UnityEngine.Events;

namespace Krk.Bum.Model.Core
{
    public class PlayerLookController
    {
        public UnityAction<string> OnBodyChanged;


        public string CurrentBodyId { get; }



    }
}
