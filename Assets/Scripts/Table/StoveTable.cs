﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTable : Table {

    [SerializeField]
    private ParticleSystem ps;

    private Transform dropPos;
    private Transform cookingPot;
    private Transform cookingPot2;

    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        chefAtTable = chef;
        SetWorkTime(action.Time);
        chef.AssignTable(this);
    }



    protected override void TableEnd () {
        ps.Stop();

    }

    protected override void TableStart () {
        ps.Play();
    }
}
