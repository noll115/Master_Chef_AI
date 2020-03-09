using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class RoundEndState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;

    private Action OnRoundEnd;
    public RoundEndState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay,Action OnRoundEnd) : base(sm, Round.RoundStates.End) {
        this.chefsInPlay = chefsInPlay;
        this.OnRoundEnd = OnRoundEnd;
    }

    // Start is called before the first frame update
    public override void OnEnter () {
    }

    public override void OnExit () {
        OnRoundEnd();
    }

    public override void Update () {
        var chefRooms = chefsInPlay.Values;
        foreach (var chefroom in chefRooms) {

        }
        sm.SwitchStateTo(Round.RoundStates.Start);
    }
}
