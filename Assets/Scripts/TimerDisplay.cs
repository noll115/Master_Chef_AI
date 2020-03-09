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

    Color endCol = Color.red;
    Color StartCol = Color.white;

    private void Awake () {
        timerStr = new StringBuilder(2);
        barTrans = timerBar.GetComponent<RectTransform>();
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
