﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
public class SeasonEndState : State<Season.SeasonStates> {
    private CanvasController canCon;
    private Action<Chef> OnSeasonEnd;
    private Chef bestChef;
    private Dictionary<uint, ChefRoom> chefsInPlay;
    public SeasonEndState (StateMachine<Season.SeasonStates> sm,Dictionary<uint,ChefRoom> chefsInPlay,CanvasController canCon,Action<Chef> OnSeasonEnd)
        : base(sm, Season.SeasonStates.End) {
        this.canCon = canCon;
        this.OnSeasonEnd = OnSeasonEnd;
        this.chefsInPlay = chefsInPlay;
    }

    public override void OnEnter () {
        var enumerator = chefsInPlay.GetEnumerator();
        enumerator.MoveNext();
        ChefRoom  bestChefRoom = enumerator.Current.Value;
        bestChef = bestChefRoom.Chef;
        bestChefRoom.transform.parent.gameObject.SetActive(false);
    }

    public override void OnExit () {
        OnSeasonEnd(bestChef);
    }

    public override void Update () {
        sm.SwitchStateTo(Season.SeasonStates.Stop);
    }
}
