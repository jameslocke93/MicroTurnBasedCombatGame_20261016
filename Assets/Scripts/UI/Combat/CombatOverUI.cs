using UnityEngine;

public class CombatOverUI : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI resultText;
    [SerializeField] private UnityEngine.UI.Button mainMenuButton;
    [SerializeField] private UnityEngine.UI.Button equipmentButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        equipmentButton.onClick.AddListener(OnEquipmentButtonClicked);
    }

    private void OnDestroy() {
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        equipmentButton.onClick.RemoveListener(OnEquipmentButtonClicked);
    }

    private void OnMainMenuButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
    }

    private void OnEquipmentButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.EquipmentScene);
    }

    public void SetCombatResult(bool playerWon) {
        if (playerWon) {
            resultText.text = "Victory!";
        } else {
            resultText.text = "Defeat!";
        }
    }
}
