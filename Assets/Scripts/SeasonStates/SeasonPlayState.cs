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
        rounds[currRound].OnRoundEnd -= OnRoundEnd;
        currRound++;
        if (currRound < numOfRounds) {
            rounds[currRound].OnRoundEnd += OnRoundEnd;
        } else {
            sm.SwitchStateTo(Season.SeasonStates.End);
        }
    }

    public override void OnEnter () {
        for (int i = 0; i < numOfRounds; i++) {
            rounds[i] = new Round(chefsInPlay,canCon);
        }
        rounds[currRound].OnRoundEnd += OnRoundEnd;
    }

    public override void Update () {
        rounds[currRound].Update();
    }

    public override void OnExit () {
    }


}
