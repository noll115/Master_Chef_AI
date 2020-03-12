using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Table : MonoBehaviour
{
    [SerializeField]
    protected Transform cookingPos;
    public abstract void DoAction (Dictionary<string,int> consummes,List<string> required);
}
