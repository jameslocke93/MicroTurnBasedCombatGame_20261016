using UnityEngine;

public class CombatPlayerStatsUI : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI playerNameText;
    [SerializeField] private TMPro.TextMeshProUGUI playerHPText;
    [SerializeField] private TMPro.TextMeshProUGUI playerMPText;

    private void Start() {
        GameManager.Instance.OnActionEvent += OnAction;
        UpdatePlayerStatsUI(GameManager.Instance.GetCurrentActor());
    }

    private void OnDestroy() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnActionEvent -= OnAction;
        }
    }

    private void OnAction(object sender, GameManager.DamageEventArgs e) {
        if (GameManager.Instance.GetCurrentActor().IsPlayable()) {
            UpdatePlayerStatsUI(GameManager.Instance.GetCurrentActor());
        } else {
            UpdatePlayerTarget(e.target);
        }
    }

    private void UpdatePlayerStatsUI(Character character) {
        if (character != null) {
            playerNameText.text = character.GetCharacterName();
            playerHPText.text = $"{character.GetCurrentHP()}/{character.GetMaxHP()}";
            playerMPText.text = $"{character.GetCurrentMP()}/{character.GetMaxMP()}";
        }
    }

    private void UpdatePlayerTarget(Character target) {
        if (target != null && target.IsPlayable()) {
            playerNameText.text = target.GetCharacterName();
            playerHPText.text = $"{target.GetCurrentHP()}/{target.GetMaxHP()}";
            playerMPText.text = $"{target.GetCurrentMP()}/{target.GetMaxMP()}";
        }
    }
}
