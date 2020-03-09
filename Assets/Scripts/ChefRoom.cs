﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ChefRoom : MonoBehaviour {
    public CookingArea cookingArea;
    [SerializeField]
    private Transform chefSpawnPos = null;

    [SerializeField]
    private GameObject chefPrefab = null;
    //chef for room
    private chef chefInRoom;

    private uint id;

    private float transitionTime;

    private Dictionary<uint, ChefRoom> chefsInPlay; 

    public uint Id { get => id; }

    public chef Chef { get => chefInRoom; }


    public void Appear (float tweenVal) {
        transitionTime = tweenVal;
        gameObject.SetActive(true);
        LeanTween.moveY(gameObject, 0, tweenVal).setEaseInOutSine();
    }

    private void OnDisappearEnd () {
        gameObject.SetActive(false);
        
    }

    public void Lost () {
        chefsInPlay.Remove(id);
        LeanTween.moveY(gameObject, -10, transitionTime).setEaseInOutSine().setOnComplete(OnDisappearEnd);
    }

    public void InitRoom (uint id,Dictionary<uint,ChefRoom> chefsInPlay) {
        this.chefsInPlay = chefsInPlay;
        this.id = id;
        this.name = $"chefRoom {id}";
        chefInRoom = Instantiate(chefPrefab, chefSpawnPos.position, Quaternion.identity, this.transform).GetComponent<chef>();
    }
}