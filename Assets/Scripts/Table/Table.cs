using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Table : MonoBehaviour {
    [SerializeField]
    protected Tables table;


    [SerializeField]
    protected Transform cookingPos;


    private float timeAtTable;
    private float currTimeAtTable;

    protected Chef chefAtTable;

    public Vector3 CookingPos { get => cookingPos.position; }


    protected List<GameObject> GOsUsed = null;

    protected virtual void Awake () {
        GOsUsed = new List<GameObject>();
    }

    public abstract void AssignTable (Chef chef,ActionDictionaries.Action action);

    protected void SetWorkTime(float time) {
        timeAtTable = time;
        currTimeAtTable = time;
    }

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
