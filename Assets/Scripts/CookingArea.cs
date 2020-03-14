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


    public void AssignTable (Chef chef, ActionDictionaries.Action action) {
        switch (action.Station) {
            case Tables.oven:
                ovenTable.AssignTable(chef, action);
                currentTableAt = ovenTable;
                break;
            case Tables.cutting:
                cuttingTable.AssignTable(chef, action);
                currentTableAt = cuttingTable;
                break;
            case Tables.stove:
                stoveTable.AssignTable(chef, action);
                currentTableAt = stoveTable;
                break;
            case Tables.blank:
                blankTable.AssignTable(chef, action);
                currentTableAt = blankTable;
                break;
            default:
                break;
        }
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
