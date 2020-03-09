using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
public class SeasonEndState : State<Season.SeasonStates> {
    private CanvasController canCon;
    private Action<chef> OnSeasonEnd;
    private chef bestChef;
    private Dictionary<uint, ChefRoom> chefsInPlay;
    public SeasonEndState (StateMachine<Season.SeasonStates> sm,Dictionary<uint,ChefRoom> chefsInPlay,CanvasController canCon,Action<chef> OnSeasonEnd)
        : base(sm, Season.SeasonStates.End) {
        this.canCon = canCon;
        this.OnSeasonEnd = OnSeasonEnd;
        this.chefsInPlay = chefsInPlay;
    }

    public override void OnEnter () {
        var enumerator = chefsInPlay.GetEnumerator();
        enumerator.MoveNext();
        bestChef = enumerator.Current.Value.Chef;
    }

    public override void OnExit () {
        OnSeasonEnd(bestChef);
    }

    public override void Update () {
        sm.SwitchStateTo(Season.SeasonStates.Play);
    }
}
