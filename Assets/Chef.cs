using System.Collections;
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
    public double fitness;

    private Transform chefTrans;
    public Transform ChefTrans { get => chefTrans; }

    public uint ID;


    private Table tableCurrAt = null;


    public bool IsBusy {
        get => tableCurrAt != null;
    }

    public void AssignTable (Table table) {
        Debug.Assert(tableCurrAt == null);
        tableCurrAt = table;
    }


    public void DoneWithTable (float score) {
        Debug.Assert(tableCurrAt != null);
        fitness += score;
        tableCurrAt = null;
    }

    public void RoundEnd () {
        if (IsBusy) {
            tableCurrAt.RoundOver();
        }
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
        GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
    }
}
