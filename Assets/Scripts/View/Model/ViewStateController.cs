using System.Collections.Generic;
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


        private Stack<ViewStateEnum> states;


        public ViewStateEnum State
        {
            get { return states.Peek(); }
        }


        public ViewStateController()
        {
            states = new Stack<ViewStateEnum>();
            states.Push(ViewStateEnum.Street);
        }

        public void SetState(ViewStateEnum state)
        {
            states.Push(state);
            if (OnStateChanged != null) OnStateChanged(State);
        }

        public void BackState()
        {
            states.Pop();
            if (OnStateChanged != null) OnStateChanged(State);
        }
    }
}
