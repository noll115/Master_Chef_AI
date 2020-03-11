using System.Collections.Generic;
using UnityEngine;


namespace StateMachine {


    public class StateMachine<T> where T : System.Enum {
        private State<T> currentState;
        private State<T> prevState;
        private Dictionary<T, State<T>> states;
        public T CurrentState { get => currentState.GetState; }



        public StateMachine () {
        }
        public void Init (Dictionary<T, State<T>> states, T initState) {
            this.states = states;
            currentState = states[initState];
            currentState.OnEnter();
        }


        public void Update () {
            currentState?.Update();
        }

        public void SwitchStateTo (T newState) {
            currentState?.OnExit();
            bool exists = states.TryGetValue(newState, out currentState);
            if (exists)
                currentState.OnEnter();
        }


    }

    public abstract class State<T> where T : System.Enum {
        protected T state;
        protected StateMachine<T> sm;
        public T GetState { get => state; }

        public State (StateMachine<T> sm, T state) {
            this.sm = sm;
            this.state = state;
        }

        public override string ToString () {
            return state.ToString();
        }

        public abstract void OnEnter ();
        public abstract void Update ();
        public abstract void OnExit ();
    }
}


