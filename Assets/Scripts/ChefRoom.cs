using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ChefRoom : MonoBehaviour {
    public CookingArea cookingArea;
    [SerializeField]
    private Transform chefSpawnPos = null;

    [SerializeField]
    private GameObject chefPrefab = null;
    //chef for room
    private Chef chefInRoom;

    private uint id;

    private float transitionTime;

    private Dictionary<uint, ChefRoom> chefsInPlay;

    public uint Id { get => id; }

    public Chef Chef { get => chefInRoom; }


    private List<ActionDictionaries.Action> actions;





    private void Awake () {
        actions = new List<ActionDictionaries.Action> {
            {new ActionDictionaries.Action(
            "Cook_Sausage",
            2f,
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_raw"] = 1},
            new List<string>(){"oil"},
            new Dictionary<string, float>(){["stove"] = 0.6f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        )
            },{
                new ActionDictionaries.Action(
            "Cut_Tomato",
            1f,
            new Dictionary<string, int> {["tomato_slices"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        )
        };
    }

    public void InitRoom (uint id, Dictionary<uint, ChefRoom> chefsInPlay) {
        this.chefsInPlay = chefsInPlay;
        this.id = id;
        this.name = $"chefRoom {id}";
        chefInRoom = Instantiate(chefPrefab, chefSpawnPos.position, Quaternion.identity, this.transform).GetComponent<Chef>();
        chefInRoom.ID = id;
    }


    public void Tick () {
        ActionDictionaries.Action actionToDo = null;

        if (Chef.IsBusy) {
            cookingArea.WorkAtTable(Time.deltaTime);
        } else {
            if (actions.Count > 0) {
                actionToDo = actions[0];
                actions.RemoveAt(0);
            }
            if (actionToDo != null)
                cookingArea.AssignTable(Chef, actionToDo);
            else {
                Chef.transform.position = Vector3.MoveTowards(Chef.ChefTrans.position, chefSpawnPos.position, 4f * Time.deltaTime);
            }
        }
    }


    public void Appear (float tweenVal, float delay) {
        transitionTime = tweenVal;
        gameObject.SetActive(true);
        LeanTween.moveY(gameObject, 0, tweenVal).setEaseInOutSine().setDelay(delay);
    }

    private void OnDisappearEnd () {
        gameObject.SetActive(false);

    }
    public void Lost () {
        chefsInPlay.Remove(id);
        LeanTween.moveY(gameObject, -10, transitionTime * 2).setEaseInCirc().setOnComplete(OnDisappearEnd);
    }


}
