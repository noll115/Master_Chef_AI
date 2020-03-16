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
        base.AssignTable(chef, action);
        foreach (var ing in action.Consumes) {
            for (int i = 0; i < ing.Value; i++) {
                string modelStr = ActionDictionaries.Ingredients[ing.Key];
                GameObject go = ModelSpawner.GetIngredientModel(chef.ID, modelStr);
                Transform goTrans = go.GetComponent<Transform>();
                goTrans.position = foodLoc.position;
                go.SetActive(true);
                GOsUsed.Add(go);
            }
        }
    }

    protected override void TableEnd () {
        for (int i = 0; i < GOsUsed.Count; i++) {
            GOsUsed[i].SetActive(false);
        }
        animator.SetTrigger(triggerID);
        ovenLight.enabled = false;
    }

    protected override void TableStart () {
        animator.SetTrigger(triggerID);
        ovenLight.enabled = true;

    }

    protected override void TableUpdate (float delta) {
        chefAtTable.ChefTrans.position = cookingPos.position;
    }
}




