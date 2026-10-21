using System;
using UnityEngine;

public class CharacterUI : MonoBehaviour {
    [SerializeField] private CharacterHealthBarUI healthBar;
    [SerializeField] private Character character;
    [SerializeField] private TargetIndicator targetIndicator;

    private void Start() {
        GameManager.Instance.OnActionEvent += OnAction;
        GameManager.Instance.OnTurnChangedEvent += OnTurnChanged;
        UpdateHealthBar(character);
        HandleTargetIndicator(GameManager.Instance.GetCurrentActor());
    }

    private void OnTurnChanged(object sender, Character e) {
        HandleTargetIndicator(e);
    }

    private void OnDestroy() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnActionEvent -= OnAction;
            GameManager.Instance.OnTurnChangedEvent -= OnTurnChanged;
        }
    }

    private void OnAction(object sender, GameManager.DamageEventArgs e) {
        UpdateHealthBar(e.target);
    }

    private void UpdateHealthBar(Character target) {
        if (target == character) {
            healthBar.UpdateHealthBar(character.GetCurrentHP(), character.GetMaxHP());
        }
    }

    private void HandleTargetIndicator(Character character) {
        if (character == this.character) {
            ShowTargetIndicator();
        } else {
            HideTargetIndicator();
        }
    }

    private void ShowTargetIndicator() {
        targetIndicator.gameObject.SetActive(true);
    }

    private void HideTargetIndicator() {
        targetIndicator.gameObject.SetActive(false);
    }

}
