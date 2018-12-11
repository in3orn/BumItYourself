using System.Collections.Generic;
using UnityEngine.Events;

namespace Krk.Bum.View.Model
{
    public enum ViewStateEnum
    {
        Street = 0,
        Settings,
        Player,
        Inventory,
        Collection,
        Item,
        Game,
        Seller,
        Store,
        Intro
    }

    public class ViewStateController
    {
        public UnityAction<ViewStateEnum> OnStateChanged;


        private Stack<ViewStateEnum> states;


        public ViewStateEnum State
        {
            get { return states.Peek(); }
        }


        public string CurrentItemId { get; set; }


        public ViewStateController()
        {
            states = new Stack<ViewStateEnum>();
        }

        public void InitState(ViewStateEnum state)
        {
            states.Push(state);
        }

        public void SetState(ViewStateEnum state)
        {
            if (states.Peek() != state)
            {
                states.Push(state);
            }
            if (OnStateChanged != null) OnStateChanged(State);
        }

        public void BackState()
        {
            states.Pop();
            if (OnStateChanged != null) OnStateChanged(State);
        }

        public void BackState(ViewStateEnum from)
        {
            while (states.Count > 1 && states.Pop() != from) ;

            if (OnStateChanged != null) OnStateChanged(State);
        }
    }
}
