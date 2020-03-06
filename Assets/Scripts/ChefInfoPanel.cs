using TMPro;
using System.Text;
using UnityEngine;

public class ChefInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI chefName = null;

    [SerializeField]
    private TextMeshProUGUI age = null;
    private StringBuilder ageStr = null;

    [SerializeField]
    private TextMeshProUGUI region = null;

    [SerializeField]
    private SkillsDisplayTable skillTable = null;




    private void Awake () {
        ageStr = new StringBuilder();
        age.SetText(ageStr);
    }

    public void DisplayChefInfo(chef chef) {
        chefName.SetText(chef.name);
        ageStr.Clear();
        ageStr.Append(Random.Range(10,90));
        age.SetText(ageStr);
        skillTable.DisplaySkillvalues(chef);
    }


}
