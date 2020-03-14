using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankTable : Table {


    public override bool AssignTable (Chef chef, ActionDictionaries.Action action, out WaitingAction wAction) {
        wAction = null;
        timeAtTable = action.Time;
        chefAtTable = chef;
        chef.AssignTable(this);
        return true;

    }

    protected override void TableEnd () {
    }

    protected override void TableStart () {
    }
}
