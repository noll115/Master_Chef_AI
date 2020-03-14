using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : Table {
    private Animator animator;
    private int cutID;
    private void Awake () {
        animator = GetComponent<Animator>();
        cutID = Animator.StringToHash("Cut");
    }

    public override bool AssignTable (Chef chef, ActionDictionaries.Action action, out WaitingAction wAction) {
        wAction = null;
        timeAtTable = action.Time;
        chefAtTable = chef;
        chef.AssignTable(this);
        return true;
    }

    protected override void TableStart () {
        animator.SetBool(cutID, true);
    }

    protected override void TableEnd () {
        animator.SetBool(cutID, false);
    }
}
