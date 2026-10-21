using UnityEngine;

public class CharacterHealthBarUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Image healthBarFill;

    public void UpdateHealthBar(int currentHP, int maxHP) {
        if (currentHP <= 0) {
            healthBarFill.fillAmount = 0;
            return;
        } else if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        float fillAmount = (float)currentHP / maxHP;
        healthBarFill.fillAmount = fillAmount;
    }
}
