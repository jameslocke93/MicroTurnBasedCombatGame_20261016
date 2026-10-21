using System;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUI : MonoBehaviour {

    public static CombatUI Instance { get; private set; }

    [SerializeField] private UnityEngine.UI.Button equipmentButton;
    [SerializeField] private UnityEngine.UI.Button attackButton;
    [SerializeField] private UnityEngine.UI.Button skillsButton;
    public event EventHandler OnAttackButtonClickedEvent;
    public event EventHandler OnSkillsButtonClickedEvent;
    public event EventHandler<SkillData> OnSkillSelectedEvent;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        equipmentButton.onClick.AddListener(OnEquipmentButtonClicked);
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        skillsButton.onClick.AddListener(OnSkillsButtonClicked);
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
    private void OnSkillsButtonClicked() {
        if (GameManager.Instance.CurrentState == GameManager.GameState.CombatOver) {
            return;
        }
        OnSkillsButtonClickedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SkillSelected(SkillData skill) {
        Debug.Log($"Skill {skill.skillName} selected!");
        OnSkillSelectedEvent?.Invoke(this, skill);
    }
}
