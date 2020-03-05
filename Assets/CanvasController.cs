using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    private RectTransform panelTrans;
    private Vector2 initPos;

    private void Awake () {
        panelTrans = panel.GetComponent<RectTransform>();
        initPos = panelTrans.anchoredPosition;
        panelTrans.anchoredPosition = new Vector2(panelTrans.rect.width,initPos.y);
        panel.SetActive(false);
    }



}
