using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using TMPro;

public class SkillsDisplayTable : MonoBehaviour {
    [SerializeField]
    private RectTransform skillNamesParent = null;

    [SerializeField]
    private RectTransform skillValuesParent = null;

    [SerializeField]
    private GameObject textPrefab = null;


    private Dictionary<CookingSkills, SkillString> skillStrings;


    private CookingSkills[] skills;

    private void Awake () {
        skillStrings = new Dictionary<CookingSkills, SkillString>();

        skills = (CookingSkills[])System.Enum.GetValues(typeof(CookingSkills));

        float childrenHeight = skillNamesParent.rect.height / skills.Length;
        for (int i = 0; i < skills.Length; i++) {
            float posY = i * -childrenHeight;
            CreateSkillPairing(skills[i], childrenHeight, posY);
        }


    }

    private void CreateSkillPairing (CookingSkills skill, float childHeight, float posY) {
        string skillNameStr = skill.ToString();
        skillNameStr = char.ToUpper(skillNameStr[0]) + skillNameStr.Substring(1);
        TextMeshProUGUI skillName = Instantiate(textPrefab).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillValue = Instantiate(textPrefab).GetComponent<TextMeshProUGUI>();

        skillName.name = skillNameStr;
        skillValue.name = skillNameStr;

        skillName.rectTransform.SetParent(skillNamesParent);
        skillValue.rectTransform.SetParent(skillValuesParent);


        skillName.rectTransform.anchoredPosition = new Vector2(0, posY);
        skillValue.rectTransform.anchoredPosition = new Vector2(0, posY);

        skillValue.rectTransform.sizeDelta = new Vector2(0, childHeight);
        skillName.rectTransform.sizeDelta = new Vector2(0, childHeight);

        skillName.SetText(skillNameStr + ":");
        skillStrings[skill] = new SkillString(skillValue);
    }

    public void DisplaySkillvalues (GameObject chef) {
        SkillString skillStr = skillStrings[CookingSkills.oven];
        skillStr.SetString(100);
    }


    private class SkillString {
        private StringBuilder strBuilder = null;
        private TextMeshProUGUI textmeshGUI;
        public SkillString (TextMeshProUGUI textMesh) {
            strBuilder = new StringBuilder();
            strBuilder.Append(0);
            textmeshGUI = textMesh;
            textMesh.SetText(strBuilder);
        }

        public void SetString (int val) {
            strBuilder.Clear();
            strBuilder.Append(val);
            textmeshGUI.SetText(strBuilder);
        }
    }
}

public enum CookingSkills {
    stove,
    oven,
    cutting,
    stirring,
    plating,
    confidence
}