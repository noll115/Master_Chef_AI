using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : Table {
    private Animator animator;
    private int cutID;

    protected override void Awake () {
        base.Awake();
        animator = GetComponent<Animator>();
        cutID = Animator.StringToHash("Cut");
    }

    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        SetWorkTime(action.Time);
        chefAtTable = chef;
        chef.AssignTable(this);
    }

    protected override void TableStart () {
        animator.SetBool(cutID, true);
    }

    protected override void TableEnd () {
        animator.SetBool(cutID, false);
    }
}
