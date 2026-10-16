using UnityEngine;

public class EquipmentUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Button CombatButton;
    [SerializeField] private UnityEngine.UI.Button MainMenuButton;

    private void Awake() {
        CombatButton.onClick.AddListener(OnCombatButtonClicked);
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnCombatButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.BattleScreenScene);
    }

    private void OnMainMenuButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
    }
}
