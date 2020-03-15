using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : Table {
    private Animator animator;
    private int cutID;
    [SerializeField]
    private ParticleSystem foodps;
    [SerializeField]
    private Transform foodLoc;
    protected override void Awake () {
        base.Awake();
        animator = GetComponent<Animator>();
        cutID = Animator.StringToHash("Cut");
    }

    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        SetWorkTime(action.Time);
        chefAtTable = chef;
        chef.AssignTable(this);
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

    protected override void TableStart () {
        animator.SetBool(cutID, true);
        foodps.Play();
    }

    protected override void TableEnd () {
        animator.SetBool(cutID, false);
        foodps.Stop();
        for (int i = 0; i < GOsUsed.Count; i++) {
            GOsUsed[i].SetActive(false);
        }
        GOsUsed.Clear();
    }
}
