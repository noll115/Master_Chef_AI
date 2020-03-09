using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private GameObject panel = null;

    [SerializeField]
    private TimerDisplay timerDisplay;

    private ChefInfoPanel chefInfoPanel;

    private Chef currentChefSelected = null;


    private void Awake () {
        chefInfoPanel = panel.GetComponent<ChefInfoPanel>();
    }


    public void InitTimer(float time) {
        timerDisplay.Init(time);
    }


    public void UpdateDisplayTimer (float time) {
        timerDisplay.UpdateTimer(time);
    }

    public void ShowRecipeOptions () {

    }

    public void RecipeOnClick () {

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
