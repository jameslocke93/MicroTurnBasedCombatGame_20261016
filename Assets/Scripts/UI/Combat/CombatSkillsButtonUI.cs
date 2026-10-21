using System;
using UnityEngine;

public class CombatSkillsButtonUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Button skillsButton;
    [SerializeField] private TMPro.TextMeshProUGUI skillNameText;
    [SerializeField] private TMPro.TextMeshProUGUI skillMPCostText;

    public void Initialise(SkillData skill) {
        skillNameText.text = skill.skillName;
        skillMPCostText.text = $"MP: {skill.mpCost}";
        skillsButton.onClick.AddListener(() => OnSkillButtonClicked(skill));
    }

    private void OnSkillButtonClicked(SkillData skill) {
        Debug.Log($"Skill {skill.skillName} clicked!");
        CombatUI.Instance.SkillSelected(skill);
    }
}
