﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour {

    //Define all skills (Gene)
    public double stove;
    public double oven;
    public double cutting;
    public double stirring;
    public double plating;
    public double confidence;

    private Transform chefTrans;
    public Transform ChefTrans { get => chefTrans; }

    private Table tableCurrAt = null;


    public bool IsBusy {
        get => tableCurrAt != null;
    }

    public void AssignTable (Table table) {
        Debug.Assert(tableCurrAt == null);
        tableCurrAt = table;
    }


    public void DoneWithTable () {
        Debug.Assert(tableCurrAt != null);
        tableCurrAt = null;
    }

    void Awake () {
        name = NameGenerator.GetRandomName();
        stove = Random.value;
        oven = Random.value;
        cutting = Random.value;
        stirring = Random.value;
        plating = Random.value;
        confidence = Random.value;

        chefTrans = GetComponent<Transform>();
    }
}
