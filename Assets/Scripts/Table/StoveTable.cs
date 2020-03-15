using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTable : Table {

    [SerializeField]
    private ParticleSystem ps;

    [SerializeField]
    private Light stoveLight;

    [SerializeField]
    private Transform foodLoc;
    [SerializeField]
    private GameObject cookingPotLarge;
    [SerializeField]
    private GameObject cookingPot2Small;

    [SerializeField]
    private Transform[] reqLocs;

    protected override void Awake () {
        base.Awake();
    }


    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        chefAtTable = chef;
        SetWorkTime(action.Time);
        chef.AssignTable(this);
        if (Random.value > 0.5) {
            cookingPotLarge.SetActive(true);
        } else {
            cookingPot2Small.SetActive(true);
        }
        int index = 0;
        foreach (var tool in action.Requires) {
            string modelStr = ActionDictionaries.Ingredients[tool];
            GameObject go = ModelSpawner.GetIngredientModel(chef.ID, modelStr);
            go.GetComponent<Transform>().position = reqLocs[index].position;
            go.SetActive(true);
            GOsUsed.Add(go);
        }
        foreach (var ing in action.Consumes) {
            for (int i = 0; i < ing.Value; i++) {
                string modelStr = ActionDictionaries.Ingredients[ing.Key];
                GameObject go = ModelSpawner.GetIngredientModel(chef.ID, modelStr);
                Transform goTrans = go.GetComponent<Transform>();
                goTrans.position = foodLoc.position ;
                goTrans.rotation = Random.rotation;
                go.SetActive(true);
                GOsUsed.Add(go);
            }
        }
    }



    protected override void TableEnd () {
        ps.Stop();
        stoveLight.enabled = false;
        cookingPotLarge.SetActive(false);
        for (int i = 0; i < GOsUsed.Count; i++) {
            GOsUsed[i].SetActive(false);
        }
        GOsUsed.Clear();
        cookingPotLarge.SetActive(false);
        cookingPot2Small.SetActive(false);
    }

    protected override void TableStart () {
        ps.Play();
        stoveLight.enabled = true;
    }
}
