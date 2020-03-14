using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTable : Table {

    [SerializeField]
    private ParticleSystem ps;

    private bool isCooking = false;

    public override bool AssignTable (Chef chef, ActionDictionaries.Action action, out WaitingAction wAction) {
        if (!isCooking) {
            wAction = null;
            return false;
        }
        chefAtTable = chef;
        chef.AssignTable(this);
        isCooking = true;
        timeAtTable = 0.1f;
        wAction = new WaitingAction(action.Produces, action.Time, Tables.stove,TurnOff);
        currWAction = wAction;
        return true;
    }


    private void TurnOff () {
        ps.Stop();
    }


    protected override void TableEnd () {
        currWAction.countDown = true;
    }

    protected override void TableStart () {
        ps.Play();
    }
}
