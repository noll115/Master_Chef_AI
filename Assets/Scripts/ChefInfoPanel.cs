using TMPro;
using System.Text;
using UnityEngine;

public class ChefInfoPanel : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI chefName = null;

    [SerializeField]
    private TextMeshProUGUI age = null;
    private StringBuilder ageStr = null;


    [SerializeField]
    private SkillsDisplayTable skillTable = null;

    private RectTransform rectTrans;




    private void Awake () {
        ageStr = new StringBuilder();
        rectTrans = GetComponent<RectTransform>();
        age.SetText(ageStr);
    }

    public void Hide () {
        LeanTween.moveX(rectTrans, rectTrans.rect.width, 0.1f);

    }

    public void Show () {
        LeanTween.moveX(rectTrans, 0, 0.1f);

    }

    public void DisplayChefInfo (Chef chef) {
        chefName.SetText(chef.name);
        ageStr.Clear();
        ageStr.Append(Random.Range(10, 90));
        age.SetText(ageStr);
        skillTable.DisplaySkillvalues(chef);
    }


}
