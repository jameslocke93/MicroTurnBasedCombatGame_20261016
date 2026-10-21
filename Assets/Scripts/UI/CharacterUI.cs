using UnityEngine;

public class CharacterUI : MonoBehaviour {
    [SerializeField] private CharacterHealthBarUI healthBar;
    [SerializeField] private Character character;

    private void Start() {
        GameManager.Instance.OnDamageEvent += OnDamageEvent;
        UpdateHealthBar(character);
    }

    private void OnDestroy() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnDamageEvent -= OnDamageEvent;
        }
    }

    private void OnDamageEvent(object sender, GameManager.DamageEventArgs e) {
        UpdateHealthBar(e.target);
    }

    private void UpdateHealthBar(Character target) {
        if (target == character) {
            healthBar.UpdateHealthBar(character.GetCurrentHP(), character.GetMaxHP());
        }
    }

    }
