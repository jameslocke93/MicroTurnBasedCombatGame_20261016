using UnityEngine;

public class CombatUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Button EquipmentButton;

    private void Awake() {
        EquipmentButton.onClick.AddListener(OnEquipmentButtonClicked);
    }

    private void OnEquipmentButtonClicked() {
        SceneLoader.LoadScene(SceneLoader.Scene.EquipmentScene);
    }
}
