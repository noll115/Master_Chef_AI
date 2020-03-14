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

    private WaitingActionList waitinglist;


    private WaitingAction currWaitingAction;


    private void Awake () {
        waitinglist = new WaitingActionList();

    }

    public void InitRoom (uint id, Dictionary<uint, ChefRoom> chefsInPlay) {
        this.chefsInPlay = chefsInPlay;
        this.id = id;
        this.name = $"chefRoom {id}";
        chefInRoom = Instantiate(chefPrefab, chefSpawnPos.position, Quaternion.identity, this.transform).GetComponent<Chef>();
    }


    public void Tick () {
        waitinglist.SubtractTime(Time.deltaTime);

        ActionDictionaries.Action actionToDo = actions[0];
        actions.RemoveAt(0);
        //do immidiate actions first
        if (currWaitingAction != null) {
            GoTowardsWAction();
        }
        if (Chef.IsBusy) {
            cookingArea.WorkAtTable(Time.deltaTime);
        } else {
            WaitingAction wAction;
            if (cookingArea.AssignTable(Chef, actionToDo, out wAction)) {
                waitinglist.Add(wAction);
            } else {
                actions.Insert(actions.Count - 1, actionToDo);
                //check if any wait are done
                if (waitinglist.HasActionsWaiting) {
                    currWaitingAction = waitinglist.GetWaitingAction();
                    GoTowardsWAction();
                }
            }
        }
    }

    private void GoTowardsWAction () {
        Chef.transform.position = Vector3.MoveTowards(Chef.transform.position, cookingArea.GetCookingPos(currWaitingAction.TableToAttend), 4f * Time.deltaTime);
        float dist = Vector3.SqrMagnitude(Chef.transform.position - cookingArea.GetCookingPos(currWaitingAction.TableToAttend));
        if (dist <= 0) {
            currWaitingAction.CallBack();
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
