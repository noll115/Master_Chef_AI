using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private GameObject panel = null;


    private ChefInfoPanel chefInfoPanel;

    private Chef currentChefSelected = null;

    [SerializeField]
    private RecipeDisplay recipeDisplay = null;

    [SerializeField]
    private TimerDisplay timerDisplay;

    [SerializeField]
    private ChefNumberDisplay chefNumberDisplay;

    private void Awake () {
        chefInfoPanel = panel.GetComponent<ChefInfoPanel>();
    }


    public void InitTimer(float time) {
        timerDisplay.Init(time);
    }

    public void ShowTimer () {
        timerDisplay.ShowTimer();
    }

    public void HideTimer () {
        timerDisplay.HideTimer();
    }

    public void InitChefNumDisplay (int numOfContestents) {
        chefNumberDisplay.Init(numOfContestents);
    }

    public void SetChefNum(int newVal) {
        chefNumberDisplay.SetNum(newVal);
    }


    public void UpdateDisplayTimer (float time) {
        timerDisplay.UpdateTimer(time);
    }

    public void ShowRecipeOptions (Action<string> callback) {
        recipeDisplay.ShowBtns(callback);
    }

    public void HideRecipeOptions() {
        recipeDisplay.HideBtns();
    }




    public void SelectedChef (Chef chef) {
        if (chef == currentChefSelected) return;
        Chef prevChef = currentChefSelected;
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
