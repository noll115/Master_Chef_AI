using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
using System.Linq;

public class RoundEndState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;

    private Action<List<uint>> OnRoundEnd;
    List<ChefRoom> eliminatedChefs;
    List<uint> bestPerforming;
    public RoundEndState (StateMachine<Round.RoundStates> sm, Dictionary<uint, ChefRoom> chefsInPlay, Action<List<uint>> OnRoundEnd)
        : base(sm, Round.RoundStates.End) {
        this.chefsInPlay = chefsInPlay;
        this.OnRoundEnd = OnRoundEnd;
    }

    // Start is called before the first frame update
    public override void OnEnter () {
        float avg = 0;
        foreach (ChefRoom cr in chefsInPlay.Values) {
            avg += (float)cr.Chef.fitness;
        }
        avg /= chefsInPlay.Count;


        int numOfChefsSelected = Mathf.CeilToInt(chefsInPlay.Count * 0.2f);


        var bestChefsInOrder = chefsInPlay.OrderByDescending(cr => cr.Value.Chef.fitness);
        var bEnumerator = bestChefsInOrder.GetEnumerator();
        int numOfSelectedBestChefs = numOfChefsSelected;
        if (!(numOfSelectedBestChefs % 2 == 0)) {
            numOfSelectedBestChefs = Mathf.Min(numOfSelectedBestChefs + 1, chefsInPlay.Count);
        }
        bestPerforming = new List<uint>(numOfChefsSelected);
        bEnumerator.MoveNext();
        for (int i = 0; i < numOfSelectedBestChefs; i++) {
            ChefRoom chefRoom = bEnumerator.Current.Value;
            bestPerforming.Add(chefRoom.Id);
            if (!bEnumerator.MoveNext()) {
                break;
            }
        }

        eliminatedChefs = new List<ChefRoom>();
        var chefsInOrder = chefsInPlay.OrderBy(cr => cr.Value.Chef.fitness);
        var enumerator = chefsInOrder.GetEnumerator();


        enumerator.MoveNext();
        for (int i = 0; i < numOfChefsSelected; i++) {
            ChefRoom chefRoom = enumerator.Current.Value;
            float fitnessVal = (float)chefRoom.Chef.fitness;
            if (Mathf.Clamp(fitnessVal, avg, 1f) != fitnessVal)
                eliminatedChefs.Add(chefRoom);
            if (!enumerator.MoveNext()) {
                break;
            }
        }

    }

    public override void OnExit () {
        OnRoundEnd(bestPerforming);
    }

    public override void Update () {
        foreach (var chef in eliminatedChefs) {
            chef.Lost();
        }
        sm.SwitchStateTo(Round.RoundStates.Stop);
    }
}
