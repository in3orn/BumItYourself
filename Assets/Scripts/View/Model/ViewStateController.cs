using UnityEngine.Events;

namespace Krk.Bum.View.Model
{
    public enum ViewStateEnum
    {
        Street = 0,
        Game,
        Settings,
        Player,
        Inventory,
        Collection,
        Item
    }

    public class ViewStateController
    {
        public UnityAction<ViewStateEnum> OnStateChanged;

        
        private ViewStateEnum state;


        public ViewStateEnum State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    if (OnStateChanged != null) OnStateChanged(state);
                }
            }
        }
    }
}
