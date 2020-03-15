using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;


    private int triggerID;
    [SerializeField]
    private Light ovenLight;
    [SerializeField]
    private Transform foodLoc;
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
        foreach (var ing in action.Consumes) {
            for (int i = 0; i < ing.Value; i++) {
                string modelStr = ActionDictionaries.Ingredients[ing.Key];
                GameObject go = ModelSpawner.GetIngredientModel(chef.ID, modelStr);
                Transform goTrans = go.GetComponent<Transform>();
                goTrans.position = foodLoc.position;
                goTrans.rotation = Random.rotation;
                go.SetActive(true);
                GOsUsed.Add(go);
            }
        }
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




