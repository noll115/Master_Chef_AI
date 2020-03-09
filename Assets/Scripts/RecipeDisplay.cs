using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeDisplay : MonoBehaviour {

    private Button[] btns;
    private TextMeshProUGUI[] texts;
    private RectTransform[] btnRectTrans;

    private RectTransform rectTrans;

    private void Awake () {
        btns = transform.GetComponentsInChildren<Button>();
        texts = new TextMeshProUGUI[btns.Length];
        btnRectTrans = new RectTransform[btns.Length];
        for (int i = 0; i < btns.Length; i++) {
            texts[i] = btns[i].GetComponentInChildren<TextMeshProUGUI>();
            btnRectTrans[i] = btns[i].GetComponent<RectTransform>();
        }
        rectTrans = GetComponent<RectTransform>();
    }

    private void Start () {
        for (int i = 0; i < btnRectTrans.Length; i++) {
            LeanTween.textAlpha(btnRectTrans[i], 0, 1f);
        }
    }
    public void ShowBtns () {
        LeanTween.moveY(rectTrans, -360, 0.2f);

    }
    public void HideBtns () {
        LeanTween.moveY(rectTrans, -360 * 2, 0.2f);
    }
}
