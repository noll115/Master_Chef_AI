using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Table : MonoBehaviour {
    [SerializeField]
    protected Transform cookingPos;

    [SerializeField]
    protected Tables table;

    protected float timeAtTable = 0f;
    private float currTimeAtTable;

    protected Chef chefAtTable;

    protected WaitingAction currWAction;

    public Vector3 CookingPos { get => cookingPos.position; }


    public abstract bool AssignTable (Chef chef,ActionDictionaries.Action action, out WaitingAction wAction);


    public void WorkAtTable (float delta) {
        chefAtTable.transform.position = Vector3.MoveTowards(chefAtTable.transform.position, CookingPos, 4f * Time.deltaTime);
        float dist = Vector3.SqrMagnitude(chefAtTable.transform.position - cookingPos.transform.position);
        if (dist <= 0) {
            if(currTimeAtTable == timeAtTable) {
                TableStart();
            }
            currTimeAtTable = Mathf.Max(currTimeAtTable - delta, 0);
            if (currTimeAtTable <= 0) {
                TableEnd();
                chefAtTable.DoneWithTable();
                chefAtTable = null;
            }
        }
    }

    protected abstract void TableStart ();

    protected abstract void TableEnd ();


}
