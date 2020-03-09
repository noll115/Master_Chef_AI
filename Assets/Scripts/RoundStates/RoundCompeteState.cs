using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundCompeteState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private CanvasController canCon;
    public RoundCompeteState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay,CanvasController canCon) 
        : base(sm, Round.RoundStates.Compete) {
        this.chefsInPlay = chefsInPlay;
        this.canCon = canCon;
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
