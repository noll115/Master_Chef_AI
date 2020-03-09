using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundStartState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    public RoundStartState (StateMachine<Round.RoundStates> sm,Dictionary<uint,ChefRoom> chefsInPlay) 
        : base(sm, Round.RoundStates.Start) {
        this.chefsInPlay = chefsInPlay;
    }

    public override void OnEnter () {
        throw new System.NotImplementedException();
    }

    public override void OnExit () {
        throw new System.NotImplementedException();
    }

    public override void Update () {
        throw new System.NotImplementedException();
    }
}
