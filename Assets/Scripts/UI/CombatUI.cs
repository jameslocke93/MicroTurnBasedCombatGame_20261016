using System;
using UnityEngine;

public class CombatUI : MonoBehaviour {

    public static CombatUI Instance { get; private set; }

    [SerializeField] private UnityEngine.UI.Button equipmentButton;
    [SerializeField] private UnityEngine.UI.Button attackButton;
    [SerializeField] private UnityEngine.UI.Image playerHealthBarFill;
    [SerializeField] private UnityEngine.UI.Image enemyHealthBarFill;

    public event EventHandler OnAttackButtonClickedEvent;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        equipmentButton.onClick.AddListener(OnEquipmentButtonClicked);
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    private void OnEquipmentButtonClicked() {
        if (GameManager.Instance.CurrentState == GameManager.GameState.CombatOver) {
            return;
        }
        SceneLoader.LoadScene(SceneLoader.Scene.EquipmentScene);
    }

    private void OnAttackButtonClicked() {
        if (GameManager.Instance.CurrentState == GameManager.GameState.CombatOver) {
            return;
        }
        OnAttackButtonClickedEvent?.Invoke(this, EventArgs.Empty);
    }
}
