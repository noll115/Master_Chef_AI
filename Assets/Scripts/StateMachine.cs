using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T>
{
    private State currentState;

    public State CurrentState { get => currentState;}

    public abstract void Update();
    public abstract void SwitchStateTo(T newState);

    public abstract class State
    {
        private T state;

        public override string ToString()
        {
            return state.ToString();
        }

        public abstract void OnEnter();
        public abstract void Update();
        public abstract void OnExit();
    }
}


