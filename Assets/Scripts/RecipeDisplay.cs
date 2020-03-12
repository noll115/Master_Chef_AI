using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RecipeDisplay : MonoBehaviour {

    private Button[] btns;
    private TextMeshProUGUI[] texts;

    private RectTransform rectTrans;

    private CanvasGroup cg;

    private void Awake () {
        rectTrans = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        btns = transform.GetComponentsInChildren<Button>();
        texts = new TextMeshProUGUI[btns.Length];
        for (int i = 0; i < btns.Length; i++) {
            texts[i] = btns[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }



    public void ShowBtns (Action<string> callback) {
        int actionLen = ActionDictionaries.Actions.Count;
        for (int i = 0; i < btns.Length; i++) {
            int index = i;
            btns[i].onClick.AddListener(() => callback(texts[i].text));
        }
        LeanTween.alphaCanvas(cg, 1, 0.2f);
        LeanTween.moveY(rectTrans, -360, 0.2f);
    }
    public void HideBtns () {
        for (int i = 0; i < btns.Length; i++) {
            btns[i].onClick.RemoveAllListeners();
        }
        LeanTween.alphaCanvas(cg, 0, 0.2f);
        LeanTween.moveY(rectTrans, -360 * 2, 0.2f);
    }
}
