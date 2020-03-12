﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour {

    [SerializeField]
    private CanvasController canvasController;

    [SerializeField]
    private GameObject chefRoomPrefab = null;

    [SerializeField]
    private GameSettings gs;

    private Round[] rounds;

    private uint currRound = 0;

    public uint CurrentRound { get => (currRound + 1); }

    private Round playingRound = null;

    private Dictionary<uint, ChefRoom> chefsInPlay;



    private void Awake () {
        chefsInPlay = GenerateInitialChefs();
        rounds = new Round[gs.NumOfRounds];
        
        rounds[currRound] = new Round(CurrentRound, chefsInPlay, gs, null, OnRoundEnd, canvasController);
        playingRound = rounds[currRound];
    }


    private void Start () {
        float contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(gs.NumOfContestants));
        float uptime = 3f / (gs.NumOfContestants / contestantsPerColumn);
        float delay = 0f;
        for (uint i = 0; i < chefsInPlay.Count; i++) {
            chefsInPlay[i].Appear(uptime, delay);
            if ((i + 1) % contestantsPerColumn == 0) {
                delay += 0.1f;
            }
        }
    }


    private void Update () {
        if (playingRound != null)
            playingRound.Update();
    }

    private Dictionary<uint, ChefRoom> GenerateInitialChefs () {
        var chefs = new Dictionary<uint, ChefRoom>(gs.NumOfContestants);
        float contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(gs.NumOfContestants));
        GameObject ChefRoomParent = new GameObject("Che Rooms");
        float x = ((-contestantsPerColumn) * 3f) + 3f;
        float z = 0;
        for (uint i = 0; i < gs.NumOfContestants; i++) {
            ChefRoom chefRoom = GameObject.Instantiate(chefRoomPrefab, new Vector3(x, -10, z), Quaternion.identity, ChefRoomParent.transform).GetComponent<ChefRoom>();
            chefs[i] = chefRoom;
            chefRoom.InitRoom(i, chefs);
            z += 4;

            if ((i + 1) % contestantsPerColumn == 0) {
                x += 6;
                z = 0;
            }

        }
        return chefs;
    }



    private void OnRoundEnd (uint[] bestChefs) {
        Debug.Log($"Round End {CurrentRound}");
        currRound++;
        if (currRound < gs.NumOfRounds) {
            rounds[currRound] = new Round(CurrentRound, chefsInPlay, gs, bestChefs, OnRoundEnd, canvasController);
            playingRound = rounds[currRound];
        }

    }

}

[System.Serializable]
public struct GameSettings {
    [Range(8, 150), SerializeField]
    private int numOfContestants;

    [SerializeField, Range(1, 30)]
    private int numOfRounds;

    [SerializeField, Range(1, 60)]
    private float maxRoundTime;

    public int NumOfContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public float MaxRoundtime { get => maxRoundTime; }

}

