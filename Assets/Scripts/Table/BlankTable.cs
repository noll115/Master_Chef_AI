using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankTable : Table {



    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        SetWorkTime(action.Time);
        chefAtTable = chef;
        chef.AssignTable(this);

    }

    protected override void TableEnd () {
    }

    protected override void TableStart () {
    }
}
