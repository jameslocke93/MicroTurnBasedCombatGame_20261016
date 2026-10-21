using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/CharacterData")]
public class CharacterData : ScriptableObject {
    public string characterName;
    public int maxHP;
    public int maxMP;
    public int attack;
    public int defense;
    public int magicAttack;
    public int speed;
    public List<SkillData> skills;
    public SkillData basicAttack;

    public bool playable;

}
