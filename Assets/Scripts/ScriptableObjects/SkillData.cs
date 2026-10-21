using UnityEngine;

[CreateAssetMenu(menuName = "RPG/SkillData")]
public class SkillData : ScriptableObject {
    public string skillName;
    public int mpCost;
    public int power;
    public DamageType damageType;
    public SkillEffectType effectType;
    public SkillTargetType targetType;
    public SkillRangeType rangeType;
}
