using System;
using UnityEngine;

public class CombatUIController : MonoBehaviour {
    [SerializeField] private CombatUI combatUI;
    [SerializeField] private CombatSkillsUI combatSkillsUI;
    [SerializeField] private CombatOverUI combatOverUI;
    [SerializeField] private CombatPlayerStatsUI combatPlayerStatsUI;

    private void Start() {
        ShowCombatUI();
        ShowCombatPlayerStatsUI();
        HideCombatOverUI();
        HideCombatSkillsUI();

        GameManager.Instance.OnCombatOverEvent += OnCombatOverEvent;
        GameManager.Instance.OnActionEvent += OnAction;
        CombatUI.Instance.OnSkillsButtonClickedEvent += OnSkillsButtonClicked;
    }

    private void OnDestroy() {
        GameManager.Instance.OnCombatOverEvent -= OnCombatOverEvent;
        GameManager.Instance.OnActionEvent -= OnAction;
        CombatUI.Instance.OnSkillsButtonClickedEvent -= OnSkillsButtonClicked;
    }

    private void OnSkillsButtonClicked(object sender, EventArgs e) {
        ShowCombatSkillsUI();
    }

    private void OnCombatOverEvent(object sender, GameManager.CombatOverEventArgs e) {
        ShowCombatOverUI(e.playerWon);
        HideCombatSkillsUI();
        HideCombatUI();
        HideCombatPlayerStatsUI();
    }

    private void OnAction(object sender, GameManager.DamageEventArgs e) {
        HideCombatSkillsUI();
    }

    private void ShowCombatSkillsUI() {
        combatSkillsUI.gameObject.SetActive(true);
        combatSkillsUI.ShowSkills(GameManager.Instance.GetCurrentActor());
    }

    private void HideCombatSkillsUI() {
        combatSkillsUI.gameObject.SetActive(false);
    }

    private void ShowCombatOverUI(bool playerWon) {
        combatOverUI.gameObject.SetActive(true);
        combatOverUI.SetCombatResult(playerWon);
    }

    private void HideCombatOverUI() {
        combatOverUI.gameObject.SetActive(false);
    }

    private void ShowCombatUI() {
        combatUI.gameObject.SetActive(true);
    }

    private void HideCombatUI() {
        combatUI.gameObject.SetActive(false);
    }

    private void ShowCombatPlayerStatsUI() {
        combatPlayerStatsUI.gameObject.SetActive(true);
    }

    private void HideCombatPlayerStatsUI() {
        combatPlayerStatsUI.gameObject.SetActive(false);
    }
}
