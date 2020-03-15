using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class Round {

    private StateMachine<RoundStates> sm;

    public Round (uint RoundNum, Dictionary<uint, ChefRoom> chefsInPlay, GameSettings gs, Action<List<uint>> onRoundEnd, CanvasController canCon) {
        sm = new StateMachine<RoundStates>();
        var states = new Dictionary<RoundStates, State<RoundStates>> {
            {RoundStates.Start, new RoundStartState(sm,chefsInPlay,gs,canCon) },
            {RoundStates.Compete, new RoundCompeteState(sm,chefsInPlay,gs,canCon)},
            {RoundStates.End,new RoundEndState(sm,chefsInPlay,onRoundEnd) }
        };
        sm.Init(states, RoundStates.Start);
    }

    public void Update () {
        sm.Update();
    }

    public void Continue () {
        sm.SwitchStateTo(RoundStates.Start);
    }

    public enum RoundStates {
        Start, //Player choose recipe
        Compete, //chefs compete
        End, //round determine winner of round
        Stop
    }
}
