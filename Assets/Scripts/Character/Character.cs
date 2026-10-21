using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] private CharacterData characterData;
    [SerializeField] private int currentHP;
    [SerializeField] private int currentMP;

    private void Awake() {
        currentHP = characterData.maxHP;
        currentMP = characterData.maxMP;
        Debug.Log($"Character {characterData.characterName} initialized with {currentHP} HP and {currentMP} MP.");
    }

    public string GetCharacterName() {
        return characterData.characterName;
    }

    public void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP < 0) {
            currentHP = 0;
        }
        Debug.Log($"{characterData.characterName} took {damage} damage and now has {currentHP} HP.");
    }

    public SkillData GetBasicAttack() {
        return characterData.skills[0];
    }

    public int GetCurrentHP() {
        return currentHP;
    }

    public int GetCurrentMP() {
        return currentMP;
    }

    public int GetMaxHP() {
        return characterData.maxHP;
    }

    public int GetAttackPower() {
        return characterData.attack;
    }
}
