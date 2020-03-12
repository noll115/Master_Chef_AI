using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {
    [SerializeField]
    private RawImage timerBar = null;
    private RectTransform barTrans;
    [SerializeField]
    private RawImage backgroundImg;
    [SerializeField]
    private TextMeshProUGUI timerText = null;

    private StringBuilder timerStr;

    private float maxTime;

    private CanvasGroup canGroup;

    private float initPos;
    private float showPos = -50;
    private RectTransform selfTrans;
    private float transitionTime = 0.3f;

    Color endCol = Color.red;
    Color StartCol = Color.white;


    private void Awake () {
        timerStr = new StringBuilder(2);
        barTrans = timerBar.GetComponent<RectTransform>();
        canGroup = GetComponent<CanvasGroup>();
        selfTrans = GetComponent<RectTransform>();
        initPos = selfTrans.anchoredPosition.y;
    }


    public void ShowTimer () {
        LeanTween.alphaCanvas(canGroup, 1, transitionTime);
        LeanTween.moveY(selfTrans, showPos, transitionTime);
;    }

    public void HideTimer () {
        LeanTween.alphaCanvas(canGroup, 0, transitionTime);
        LeanTween.moveY(selfTrans, initPos, transitionTime);
    }

    public void Init (float maxTime) {
        this.maxTime = maxTime;
        this.gameObject.SetActive(true);
        UpdateTimer(maxTime);
    }


    public void UpdateTimer (float curTime) {
        timerStr.Clear();
        timerStr.Append(Mathf.CeilToInt(curTime));
        timerText.SetText(timerStr);
        barTrans.localScale = new Vector3(curTime / maxTime, 1, 1);
        Color col = Color.Lerp(StartCol, endCol, 1 - curTime / maxTime);
        timerBar.color = col;
        backgroundImg.color = col;
    }
}
