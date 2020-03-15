using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;


    private int triggerID;
    [SerializeField]
    private Light ovenLight;

    protected override void Awake () {
        base.Awake();
        animator = GetComponent<Animator>();
        triggerID = Animator.StringToHash("Door");
    }


    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {

        chefAtTable = chef;
        SetWorkTime(action.Time);
        chef.AssignTable(this);
        chef.ChefTrans.SetParent(cookingPos, true);
    }

    protected override void TableEnd () {
        chefAtTable.ChefTrans.SetParent(null, true);
        animator.SetTrigger(triggerID);
        ovenLight.enabled = false;
    }

    protected override void TableStart () {
        animator.SetTrigger(triggerID);
        ovenLight.enabled = true;

    }





}




