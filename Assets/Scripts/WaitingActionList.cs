using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaitingActionList {

    private List<WaitingAction> list;

    private Queue<WaitingAction> queue;


    public bool HasActionsWaiting {
        get => queue.Count > 0;
    }

    public WaitingActionList () {
        list = new List<WaitingAction>();
        queue = new Queue<WaitingAction>();
    }

    public void SubtractTime (float delta) {
        for (int i = 0; i < list.Count; i++) {
            var waitingAction = list[i];
            if (waitingAction.countDown)
                waitingAction.SubtractTime(delta);
            if (waitingAction.IsDone) {
                list.RemoveAt(i);
                queue.Enqueue(waitingAction);
            }
        }
    }

    public void Add (WaitingAction wAction) {
        if (wAction == null) return;
        list.Add(wAction);
    }

    public WaitingAction GetWaitingAction () {
        return queue.Dequeue();
    }




}

public class WaitingAction {
    private Dictionary<string, int> produced;
    private float timeleft;
    private Tables tableToAttend;

    public bool countDown = false;

    private Action callBack;

    public Action CallBack {
        get => callBack;
    }

    public bool IsDone {
        get => timeleft <= 0;
    }

    public Tables TableToAttend {
        get => tableToAttend;
    }

    public WaitingAction (Dictionary<string, int> produced, float timeleft, Tables tableToAttend,Action callBack) {
        this.produced = produced;
        this.timeleft = timeleft;
        this.tableToAttend = tableToAttend;
        this.callBack = callBack;
    }

    public void SubtractTime (float delta) {
        timeleft = Mathf.Max(timeleft - delta, 0);
    }



}

