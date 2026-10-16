using UnityEngine;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Button CombatButton;

    private void Awake() {
        CombatButton.onClick.AddListener(OnCombatButtonClicked);
    }

    private void OnCombatButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.BattleScreenScene);
    }
}
