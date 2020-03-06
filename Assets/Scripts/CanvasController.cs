using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel = null;

    private ChefInfoPanel chefInfoPanel;

    private chef currentChefSelected = null;

    private Animator animator;

    private void Awake () {
        chefInfoPanel = panel.GetComponent<ChefInfoPanel>();
        panel.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void SelectedChef(chef chef) {
        if (chef == currentChefSelected) return;
        chef prevChef = currentChefSelected;
        currentChefSelected = chef;
        if (currentChefSelected) {
            chefInfoPanel.DisplayChefInfo(currentChefSelected);
            if (!prevChef)
                animator.SetTrigger("triggerPanel");
        } else {
            if(prevChef)
                animator.SetTrigger("triggerPanel");
        }
    }

}
