using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;


    private int triggerID;

    private void Awake () {
        animator = GetComponent<Animator>();
        triggerID = Animator.StringToHash("Door");
    }


    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        chefAtTable = chef;
        SetWorkTime(action.Time);
        chef.AssignTable(this);
    }

    protected override void TableEnd () {
        animator.SetTrigger(triggerID);
    }

    protected override void TableStart () {
        animator.SetTrigger(triggerID);
    }





}




