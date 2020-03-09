using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundCompeteState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private CanvasController canCon;
    private float maxRoundTime;
    private float currTime;
    public RoundCompeteState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay, float maxRoundTime, CanvasController canCon)
        : base(sm, Round.RoundStates.Compete) {
        this.chefsInPlay = chefsInPlay;
        this.canCon = canCon;
        this.maxRoundTime = maxRoundTime;
    }

    public override void OnEnter () {
        currTime = maxRoundTime;
    }

    public override void OnExit () {
    }

    public override void Update () {
        currTime = Mathf.Max(currTime - Time.deltaTime, 0);
        canCon.UpdateDisplayTimer(currTime);
        foreach (var chefRoom in chefsInPlay.Values) {
            chefRoom.Tick();
        }
        if(currTime <= 0) {
            sm.SwitchStateTo(Round.RoundStates.End);
        }
    }
}
