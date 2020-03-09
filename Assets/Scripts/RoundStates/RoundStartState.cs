using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundStartState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private CanvasController canCon;
    private float maxRoundTime;
    public RoundStartState (StateMachine<Round.RoundStates> sm,Dictionary<uint,ChefRoom> chefsInPlay,float maxRoundTime, CanvasController canCon) 
        : base(sm, Round.RoundStates.Start) {
        this.chefsInPlay = chefsInPlay;
        this.canCon = canCon;
        this.maxRoundTime = maxRoundTime;
    }

    public override void OnEnter () {
        canCon.InitTimer(maxRoundTime);
        canCon.ShowRecipeOptions();
    }

    public override void OnExit () {
    }

    private void OnRecipeSelect() {

    }

    public override void Update () {
        
    }
}
