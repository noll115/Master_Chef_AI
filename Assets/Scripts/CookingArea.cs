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


    public void DoAction (Chef chef,Tables tableToDoAction) {
        switch (tableToDoAction) {
            case Tables.oven:
                break;
            case Tables.cutting:
                break;
            case Tables.stove:
                break;
            case Tables.blank:
                break;
            default:
                break;
        }
    }

}

public enum Tables {
    oven,
    cutting,
    stove,
    blank
}
