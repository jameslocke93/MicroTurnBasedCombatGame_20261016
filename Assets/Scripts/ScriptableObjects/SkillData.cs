using UnityEngine;

[CreateAssetMenu(menuName = "RPG/SkillData")]
public class SkillData : ScriptableObject {
    public string skillName;
    public int mpCost;
    public int power;
}
