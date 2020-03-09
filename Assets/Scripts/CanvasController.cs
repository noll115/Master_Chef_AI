﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private GameObject panel = null;

    [SerializeField]
    private TimerDisplay timerDisplay;

    private ChefInfoPanel chefInfoPanel;

    private RectTransform panelTrans;


    private chef currentChefSelected = null;


    private void Awake () {
        chefInfoPanel = panel.GetComponent<ChefInfoPanel>();
        panelTrans = chefInfoPanel.GetComponent<RectTransform>();
    }


    public void InitTimer(float time) {
        timerDisplay.Init(time);
    }


    public void UpdateDisplayTimer (float time) {
        timerDisplay.UpdateTimer(time);
    }



    public void SelectedChef (chef chef) {
        if (chef == currentChefSelected) return;
        chef prevChef = currentChefSelected;
        currentChefSelected = chef;
        if (currentChefSelected) {
            chefInfoPanel.DisplayChefInfo(currentChefSelected);
            if (!prevChef)
                chefInfoPanel.Show();
        } else {
            if (prevChef) {
                chefInfoPanel.Hide();
            }
        }
    }


}