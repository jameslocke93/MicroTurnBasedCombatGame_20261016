using System.Collections.Generic;
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

    public void Heal(int healAmount) {
        currentHP += healAmount;
        if (currentHP > characterData.maxHP) {
            currentHP = characterData.maxHP;
        }
        Debug.Log($"{characterData.characterName} healed {healAmount} HP and now has {currentHP} HP.");
    }

    public void UseMP(int mpCost) {
        currentMP -= mpCost;
        if (currentMP < 0) {
            currentMP = 0;
        }
        Debug.Log($"{characterData.characterName} used {mpCost} MP and now has {currentMP} MP.");
    }

    public SkillData GetBasicAttack() {
        return characterData.basicAttack;
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

    public int GetMaxMP() {
        return characterData.maxMP;
    }

    public int GetAttackPower() {
        return characterData.attack;
    }

    public int GetDefense() {
        return characterData.defense;
    }

    public int GetSpeed() {
        return characterData.speed;
    }

    public int GetMagicPower() {
        return characterData.magicAttack;
    }

    public List<SkillData> GetSkills() {
        return characterData.skills;
    }

    public bool IsPlayable() {
        return characterData.playable;
    }
}
