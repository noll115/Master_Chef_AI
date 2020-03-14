using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelSpawner {

    private static Dictionary<string, GameObject[]> ingredientModels;
    private static Dictionary<string, GameObject[]> toolModels;
    private static GameObject ingParent;
    private static GameObject toolParent;

    public static void Init () {

    }

    static ModelSpawner () {
        ingParent = new GameObject("Ingredients");
        toolParent = new GameObject("Tools");
        Transform ingParentTrans = ingParent.GetComponent<Transform>();
        Transform toolParentTrans = toolParent.GetComponent<Transform>();
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

        toolModels = new Dictionary<string, GameObject[]>(ActionDictionaries.Tools.Count);
        foreach (var tool in ActionDictionaries.Tools) {
            string model = tool.Value;
            GameObject toolModel = Resources.Load(model) as GameObject;
            Debug.Assert(toolModel != null);
            toolModels[model] = new GameObject[NumOfChefs];
            for (int i = 0; i < NumOfChefs; i++) {
                toolModels[model][i] = GameObject.Instantiate(toolModel, toolParentTrans);
                toolModels[model][i].SetActive(false);
            }
        }
    }


    public static GameObject GetToolModel(uint id,string toolModelName) {
        return toolModels[toolModelName][id];
    }

    public static GameObject GetIngredientModel(uint id,string ingModelName) {
        return ingredientModels[ingModelName][id];
    }


}
