using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTable : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform cookingPos;
    private void Awake () {
        animator = GetComponent<Animator>();
    }
}
