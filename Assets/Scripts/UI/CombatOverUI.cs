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

    private void Start() {
        GameManager.Instance.OnCombatOverEvent += OnCombatOverEvent;
        Hide();
    }

    private void OnMainMenuButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
    }

    private void OnEquipmentButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.EquipmentScene);
    }

    private void OnCombatOverEvent(object sender, GameManager.CombatOverEventArgs e) {
        if (e.playerWon) {
            resultText.text = "Victory!";
        } else {
            resultText.text = "Defeat!";
        }
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
