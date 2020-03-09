using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
public class SeasonPlayState : State<Season.SeasonStates> {


    private CanvasController canCon;
    private Round[] rounds;
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private uint currRound = 0;
    private int numOfRounds;
    private float maxRoundTime;
    public SeasonPlayState (StateMachine<Season.SeasonStates> sm, Dictionary<uint, ChefRoom> chefsInPlay, GameSettings gs, Round[] rounds, CanvasController canCon)
        : base(sm, Season.SeasonStates.Play) {
        this.canCon = canCon;
        this.rounds = rounds;
        this.chefsInPlay = chefsInPlay;
        this.numOfRounds = gs.NumOfRounds;
        this.maxRoundTime = gs.MaxRoundtime;
    }

    private void OnRoundEnd () {
        Debug.Log($"ROUND {currRound} END ");
        currRound++;
        if (currRound >= numOfRounds) {
            sm.SwitchStateTo(Season.SeasonStates.End);
        } else {
            Debug.Log($"ROUND {currRound} START ");
            rounds[currRound] = new Round(chefsInPlay, canCon, maxRoundTime, OnRoundEnd);
        }
    }

    public override void OnEnter () {
        rounds[0] = new Round(chefsInPlay, canCon, maxRoundTime, OnRoundEnd);
    }

    public override void Update () {
        rounds[currRound].Update();
    }

    public override void OnExit () {
    }


}
