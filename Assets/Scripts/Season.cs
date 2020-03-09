using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class Season {

    private StateMachine<SeasonStates> sm;

    private Dictionary<uint, ChefRoom> chefs;

    private int maxContestants;
    private int currentNumOfContestants;

    private GameObject chefRoomPrefab;

    private GameObject chefPrefab;

    private Round[] rounds;

    public event Action<chef> OnSeasonEnd;


    public Season (GameStats gameStats, GameObject chefRoomPrefab,chef prevWinChef) {

        chefs = new Dictionary<uint, ChefRoom>(maxContestants);
        rounds = new Round[gameStats.NumOfRounds];
        this.chefRoomPrefab = chefRoomPrefab;
        this.maxContestants = gameStats.maxContestants;
        this.currentNumOfContestants = maxContestants;
        sm = new StateMachine<SeasonStates>();

        var states = new Dictionary<SeasonStates, State<SeasonStates>> {
            { SeasonStates.Start,new SeasonStartState(sm,chefs,gameStats,chefRoomPrefab,prevWinChef) },
            { SeasonStates.Play,new SeasonPlayState(sm,chefs) },

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
        End    //determine beast chef of season
    }
}




