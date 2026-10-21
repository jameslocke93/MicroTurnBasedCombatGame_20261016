using System;
using UnityEngine;

public class CombatSkillsUI : MonoBehaviour {
    [SerializeField] private Transform skillsButtonContainer;
    [SerializeField] private CombatSkillsButtonUI skillButtonPrefab;
    public void ShowSkills(Character character) {
        // Clear existing buttons
        foreach (Transform child in skillsButtonContainer) {
            Destroy(child.gameObject);
        }
        // Create a button for each skill
        foreach (var skill in character.GetSkills()) {
            CombatSkillsButtonUI button = Instantiate(skillButtonPrefab, skillsButtonContainer);
            button.Initialise(skill);
        }
    }
}
