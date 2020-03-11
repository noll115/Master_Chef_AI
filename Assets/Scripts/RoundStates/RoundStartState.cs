using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class RoundStartState : State<Round.RoundStates> {
    private Dictionary<uint, ChefRoom> chefsInPlay;
    private readonly GameSettings gs;
    private CanvasController canCon;
    private float maxRoundTime;
    private GameObject roundParent;
    private float contestantsPerColumn;


    
    public RoundStartState (StateMachine<Round.RoundStates> sm,Dictionary<uint,ChefRoom> chefsInPlay,GameSettings gs, CanvasController canCon) 
        : base(sm, Round.RoundStates.Start) {
        this.chefsInPlay = chefsInPlay;
        this.gs = gs;
        this.canCon = canCon;
        this.maxRoundTime = gs.MaxRoundtime;
        
    }

    public override void OnEnter () {
        canCon.InitTimer(maxRoundTime);
        canCon.ShowRecipeOptions(OnRecipeSelect);

    }

    public override void OnExit () {
    }

    private void OnRecipeSelect(string returnVal) {
        Debug.Log(returnVal);
        foreach (ChefRoom chefRoom in chefsInPlay.Values) {
            //set recipe target for chefs in competition
        }
        canCon.HideRecipeOptions();
        sm.SwitchStateTo(Round.RoundStates.Compete);
    }

    public override void Update () {
        
    }

}
