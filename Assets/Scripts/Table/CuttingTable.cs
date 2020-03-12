using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : Table {
    private Animator animator;

    public override void DoAction (Dictionary<string, int> consummes, List<string> required) {
        throw new System.NotImplementedException();
    }

    private void Awake () {
        animator = GetComponent<Animator>();
    }
}
