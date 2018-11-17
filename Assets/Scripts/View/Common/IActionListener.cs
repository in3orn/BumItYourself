using UnityEngine.UI;

namespace Krk.Bum.Common
{
    public interface IButtonListener
    {
        void Subscribe(Button action);
        void Unsubscribe(Button action);
    }
}
