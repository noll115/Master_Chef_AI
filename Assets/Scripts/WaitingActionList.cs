using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaitingActionList : List<WaitingAction> {
    public WaitingActionList (int capacity) : base(capacity) {
    }

    public bool SubtractTime (float delta, Queue<WaitingAction> queue) {
        int addedToQueue = 0;
        for (int i = 0; i < this.Count; i++) {
            var waitingAction = this[i];
            waitingAction.SubtractTime(delta);
            if (waitingAction.IsDone) {
                queue.Enqueue(waitingAction);
                addedToQueue++;
            }
        }
        return addedToQueue > 0;
    }

}

public class WaitingAction {
    private Dictionary<string, int> produced;
    private float timeleft;
    private Tables tableToAttend;

    public bool IsDone {
        get => timeleft <= 0;
    }

    public WaitingAction (Dictionary<string, int> produced, float timeleft, Tables tableToAttend) {
        this.produced = produced;
        this.timeleft = timeleft;
        this.tableToAttend = tableToAttend;
    }

    public void SubtractTime (float delta) {
        timeleft = Mathf.Max(timeleft - delta, 0);
    }
}

