using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingArea : MonoBehaviour {

    [SerializeField]
    private OvenTable ovenTable;
    [SerializeField]
    private CuttingTable cuttingTable;
    [SerializeField]
    private StoveTable stoveTable;
    [SerializeField]
    private BlankTable blankTable;


    private Table currentTableAt;


    public bool AssignTable (Chef chef, ActionDictionaries.Action action, out WaitingAction wAction) {
        bool res = false;
        wAction = null;
        switch (action.Station) {
            case Tables.oven:
                res = ovenTable.AssignTable(chef, action, out wAction);
                if (res)
                    currentTableAt = ovenTable;
                break;
            case Tables.cutting:
                res = cuttingTable.AssignTable(chef, action, out wAction);
                if (res)
                    currentTableAt = cuttingTable;
                break;
            case Tables.stove:
                res = stoveTable.AssignTable(chef, action, out wAction);
                if (res)
                    currentTableAt = stoveTable;
                break;
            case Tables.blank:
                res = blankTable.AssignTable(chef, action, out wAction);
                if (res)
                    currentTableAt = blankTable;
                break;
            default:
                break;
        }
        return res;
    }


    public void FinishAction () {
    }




    public void WorkAtTable (float delta) {
        currentTableAt.WorkAtTable(delta);
    }


    public Vector3 GetCookingPos (Tables table) {
        Vector3 pos = Vector3.zero;
        switch (table) {
            case Tables.oven:
                pos = ovenTable.CookingPos;
                break;
            case Tables.cutting:
                pos = cuttingTable.CookingPos;
                break;
            case Tables.stove:
                pos = stoveTable.CookingPos;
                break;
            case Tables.blank:
                pos = blankTable.CookingPos;
                break;
            default:
                break;
        }
        return pos;
    }
}
public enum Tables {
    oven,
    cutting,
    stove,
    blank
}
