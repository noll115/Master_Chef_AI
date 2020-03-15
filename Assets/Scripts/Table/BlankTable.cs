using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankTable : Table {



    [SerializeField]
    private Transform foodLoc;
    public override void AssignTable (Chef chef, ActionDictionaries.Action action) {
        SetWorkTime(action.Time);
        chefAtTable = chef;
        chef.AssignTable(this);
        foreach (var ing in action.Consumes) {
            for (int i = 0; i < ing.Value; i++) {
                string modelStr = ActionDictionaries.Ingredients[ing.Key];
                GameObject go = ModelSpawner.GetIngredientModel(chef.ID, modelStr);
                Transform goTrans = go.GetComponent<Transform>();
                Vector2 randPos = Random.insideUnitCircle;
                goTrans.position = foodLoc.position + new Vector3(randPos.x,0,randPos.y);
                go.SetActive(true);
                GOsUsed.Add(go);
            }
        }
    }

    protected override void TableEnd () {
        foreach (var go in GOsUsed) {
            go.SetActive(false);
        }
        GOsUsed.Clear();
    }

    protected override void TableStart () {
    }
}
