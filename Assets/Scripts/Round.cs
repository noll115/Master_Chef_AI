using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class Round {

    private StateMachine<RoundStates> sm;

    public event Action OnRoundEnd;

    private Dictionary<uint, ChefRoom> chefsInPlay;

    public Round (Dictionary<uint, ChefRoom> chefsInPlay, CanvasController canCon) {
        this.chefsInPlay = chefsInPlay;
        sm = new StateMachine<RoundStates>();
        var states = new Dictionary<RoundStates, State<RoundStates>> {
            {RoundStates.Start, new RoundStartState(sm,chefsInPlay) },
            {RoundStates.Compete, new RoundCompeteState(sm,chefsInPlay,canCon)},
            {RoundStates.End,new RoundEndState(sm,chefsInPlay,OnRoundEnd) }
        };
        sm.Init(states, RoundStates.Start);
    }

    public void Update () {
        sm.Update();
    }

    public enum RoundStates {
        Start, //Player choose recipe
        Compete, //chefs compete
        End //round determine winner of round
    }
}
