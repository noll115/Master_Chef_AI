using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
public class SeasonPlayState : State<Season.SeasonStates> {


    public SeasonPlayState (StateMachine<Season.SeasonStates> sm, Dictionary<uint, ChefRoom> chefs)
        : base(sm, Season.SeasonStates.Play) {

    }

    public override void OnEnter () {

    }

    public override void OnExit () {
    }

    public override void Update () {
    }
}
