using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelSpawner {

    private static Dictionary<string, GameObject[]> ingredientModels;
    private static Dictionary<string, GameObject[]> toolModels;
    private static GameObject ingParent;


    public static void Init () {

    }

    static ModelSpawner () {
        ingParent = new GameObject("Ingredients");
        Transform ingParentTrans = ingParent.GetComponent<Transform>();
        int NumOfChefs = GameHandler.Instance.GameSettings.NumOfContestants;
        ingredientModels = new Dictionary<string, GameObject[]>(ActionDictionaries.Ingredients.Count);
        foreach (var ing in ActionDictionaries.Ingredients) {
            string model = ing.Value;
            GameObject ingModel = Resources.Load(model) as GameObject;
            ingredientModels[model] = new GameObject[NumOfChefs];
            for (int i = 0; i < NumOfChefs; i++) {
                ingredientModels[model][i] = GameObject.Instantiate(ingModel, ingParentTrans);
                ingredientModels[model][i].SetActive(false);
            }
        }

    }

    public static GameObject GetIngredientModel(uint id,string ingModelName) {
        return ingredientModels[ingModelName][id];
    }


}
