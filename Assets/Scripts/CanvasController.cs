using System.Collections;
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
        panel.SetActive(false);
    }


    public void UpdateDisplayTimer () {

    }



    public void SelectedChef (chef chef) {
        if (chef == currentChefSelected) return;
        chef prevChef = currentChefSelected;
        currentChefSelected = chef;
        if (currentChefSelected) {
            chefInfoPanel.DisplayChefInfo(currentChefSelected);
            if (!prevChef)
                panel.SetActive(true);
                panelTrans.anchoredPosition = new Vector2(0, 0);
        } else {
            if (prevChef) {
                panel.SetActive(false);
                panelTrans.anchoredPosition = new Vector2(panelTrans.rect.width, 0);
            }
        }
    }


}
