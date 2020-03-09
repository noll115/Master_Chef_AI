using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class Season {

    private StateMachine<SeasonStates> sm;


    private ChefRoom[] totalChefs;
    private Dictionary<uint, ChefRoom> chefsInPlay;

    private int maxContestants;
    private int currentNumOfContestants;

    private GameObject chefRoomPrefab;


    private Round[] rounds;


    public SeasonStates currSeasonState {
        get => sm.CurrentState;
    }



    public Season (uint seasonNum, GameSettings gameSettings, GameObject chefRoomPrefab, Chef prevWinChef, CanvasController canCon,Action<Chef> onSeasonEnd) {

        this.maxContestants = gameSettings.maxContestants;
        this.currentNumOfContestants = maxContestants;
        this.chefRoomPrefab = chefRoomPrefab;


        rounds = new Round[gameSettings.NumOfRounds];
        chefsInPlay = new Dictionary<uint, ChefRoom>(maxContestants);
        totalChefs = new ChefRoom[maxContestants];
        sm = new StateMachine<SeasonStates>();

        var states = new Dictionary<SeasonStates, State<SeasonStates>> {
            { SeasonStates.Start, new SeasonStartState(sm,seasonNum,totalChefs,chefsInPlay,gameSettings,chefRoomPrefab,prevWinChef,canCon) },
            { SeasonStates.Play,  new SeasonPlayState(sm,chefsInPlay,gameSettings,rounds,canCon) },
            { SeasonStates.End,   new SeasonEndState(sm,chefsInPlay,canCon,onSeasonEnd) }
            };
        sm.Init(states, SeasonStates.Start);
    }

    public void SeasonStart () {

    }


    public void Update () {
        sm.Update();
    }




    public enum SeasonStates {
        Start, //Generate chefs
        Play,  //Go through rounds
        End,    //determine beast chef of season
        Stop
    }
}




