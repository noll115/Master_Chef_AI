﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RecipeDisplay : MonoBehaviour {

    [SerializeField]
    private Button[] btns;
    private TextMeshProUGUI[] texts;

    private RectTransform rectTrans;

    private CanvasGroup cg;

    private List<string> choices;

    private void Awake () {
        rectTrans = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        btns = transform.GetComponentsInChildren<Button>();
        texts = new TextMeshProUGUI[btns.Length];
        for (int i = 0; i < btns.Length; i++) {
            texts[i] = btns[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        choices = new List<string>(ActionDictionaries.Meals.Keys);
    }



    public void ShowBtns (Action<string> callback) {
        Debug.Log(choices);
        int actionLen = ActionDictionaries.Actions.Count;
        for (int i = 0; i < btns.Length; i++) {
            int index = UnityEngine.Random.Range(0,choices.Count);
            texts[i].SetText(choices[index]);
            btns[i].onClick.AddListener(() => callback(choices[index]));
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
