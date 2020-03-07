using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;


public class Season {

    private StateMachine<SeasonStates> sm;

    private Dictionary<uint, ChefRoom> chefs;

    private int maxContestants;

    private GameObject chefRoomPrefab;

    private GameObject chefPrefab;

    private Round[] rounds;

    private int currRound = 0;

    public Season (GameStats gameStats,GameObject chefRoomPrefab,GameObject chefPrefab) {

        chefs = new Dictionary<uint, ChefRoom>(maxContestants);
        rounds = new Round[gameStats.NumOfRounds];
        this.chefPrefab = chefPrefab;
        this.chefRoomPrefab = chefRoomPrefab;
        this.maxContestants = gameStats.NumOfContestants;

        var states = new Dictionary<SeasonStates, State<SeasonStates>>(
            );
        sm = new StateMachine<SeasonStates>(states, SeasonStates.Start);
    }


    public void Update () {

    }


    private void SpawnChefs () {
        int contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(maxContestants));
        int x = 0, z = 0;
        GameObject roomParent = new GameObject("Chef Rooms");
        GameObject chefsGO = new GameObject("Chefs");
        for (uint i = 1; i <= maxContestants; i++) {

            ChefRoom room = GameObject.Instantiate(chefRoomPrefab, new Vector3(x, 0, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();

            chef chef = GameObject.Instantiate(chefPrefab, chefsGO.transform).GetComponent<chef>();
            chef.name = $"Chef {i}";

            room.InitRoom(chef, i);
            chefs[i] = room;
            z += 4;

            if (i % contestantsPerColumn == 0) {
                x += 6;
                z = 0;
            }
        }
    }

    private enum SeasonStates {
        Start,
        Rounds,
        End
    }
}




