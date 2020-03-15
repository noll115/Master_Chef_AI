using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundCompeteState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private readonly GameSettings gs;
    private CanvasController canCon;
    private float currTime;
    public RoundCompeteState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay, GameSettings gs, CanvasController canCon)
        : base(sm, Round.RoundStates.Compete) {
        this.chefsInPlay = chefsInPlay;
        this.gs = gs;
        this.canCon = canCon;
    }

    public override void OnEnter () {
        canCon.ShowTimer();
        currTime = gs.MaxRoundtime;
    }

    public override void OnExit () {
        canCon.HideTimer();
        foreach (var chefRoom in chefsInPlay) {
            chefRoom.Value.Chef.RoundEnd();
        }
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
