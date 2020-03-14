using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class RoundEndState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;

    private Action<uint[]> OnRoundEnd;
    public RoundEndState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay, Action<uint[]> OnRoundEnd)
        : base(sm, Round.RoundStates.End) {
        this.chefsInPlay = chefsInPlay;
        this.OnRoundEnd = OnRoundEnd;
    }

    // Start is called before the first frame update
    public override void OnEnter () {
    }

    public override void OnExit () {
        OnRoundEnd(null);
    }

    public override void Update () {
        var chefRooms = chefsInPlay.Values;
        List<ChefRoom> eliminatedChefs = new List<ChefRoom>();
        int NumOfChefsElmiminated = Mathf.CeilToInt(chefsInPlay.Count * 0.2f);
        foreach (var chefroom in chefRooms) {
            if (UnityEngine.Random.value < 0.2f) {
                eliminatedChefs.Add(chefroom);
            }
        }
        foreach (var chefRoom in eliminatedChefs) {
            chefRoom.Lost();
        }
        sm.SwitchStateTo(Round.RoundStates.Stop);
    }
}
