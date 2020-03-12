using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : Table {


    private Animator animator;

    public override void DoAction (Dictionary<string, int> consummes, List<string> required) {

    }

    private void Awake () {
        animator = GetComponent<Animator>();
    }
}
