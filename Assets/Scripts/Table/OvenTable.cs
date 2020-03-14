using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;

    private bool isCooking = false;

    private int triggerID;

    private void Awake () {
        animator = GetComponent<Animator>();
        triggerID = Animator.StringToHash("Door");
    }


    public override bool AssignTable (Chef chef, ActionDictionaries.Action action, out WaitingAction wAction) {
        if (!isCooking) {
            wAction = null;
            return false;
        }
        chefAtTable = chef;
        chef.AssignTable(this);
        isCooking = true;
        timeAtTable = 0.1f;
        wAction = new WaitingAction(action.Produces, action.Time, Tables.oven, TakeOutIngredients);
        currWAction = wAction;
        return true;
    }


    private void TakeOutIngredients () {
        animator.SetTrigger(triggerID);
    }

    protected override void TableEnd () {
    }

    protected override void TableStart () {
        animator.SetTrigger(triggerID);
    }





}




