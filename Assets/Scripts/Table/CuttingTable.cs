using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : MonoBehaviour
{
    [SerializeField]
    private Transform cookingPos;
    private Animator animator;
    private void Awake () {
        animator = GetComponent<Animator>();
    }
}
