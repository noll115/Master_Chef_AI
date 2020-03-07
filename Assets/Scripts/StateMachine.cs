using System.Collections.Generic;
using UnityEngine;


namespace StateMachine {


    public class StateMachine<T> where T : System.Enum {
        private State<T> currentState;
        private readonly Dictionary<T, State<T>> states;
        public State<T> CurrentState { get => currentState; }



        public StateMachine (Dictionary<T, State<T>> states, T initState) {
            this.states = states;
            currentState = states[initState];
        }


        public void Update () {
            currentState.Update();
        }

        public void SwitchStateTo (T newState) {
            currentState.OnExit();
            currentState = states[newState];
            currentState.OnEnter();
        }


    }

    public abstract class State<T> where T : System.Enum {
        protected T state;

        public T GetState { get => state; }

        public override string ToString () {
            return state.ToString();
        }

        public abstract void OnEnter ();
        public abstract void Update ();
        public abstract void OnExit ();
    }
}


