using UnityEngine;

public static class SceneLoader {
    public enum Scene {
        MainMenuScene,
        BattleScreenScene,
        EquipmentScene,
    }

    public static void LoadScene(Scene scene) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }
}
