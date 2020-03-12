using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;


    private void Awake () {
        animator = GetComponent<Animator>();
    }
    public override void DoAction (Dictionary<string, int> consumes, List<string> required) {

    }
}




