using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ChefNumberDisplay : MonoBehaviour
{
    private StringBuilder strBuilder;
    [SerializeField]
    private TextMeshProUGUI numText;
    private void Awake () {
        strBuilder = new StringBuilder(3);
    }

    public void Init (int num) {
        strBuilder.Append(num);
        numText.SetText(strBuilder);
    }

    public void SetNum(int newVal) {
        strBuilder.Clear();
        strBuilder.Append(newVal);
        numText.SetText(strBuilder);
    }


}
