using UnityEngine;

public static class SkillResolver{
    public static bool ResolveSkill(Character user, Character target, SkillData skill) {
        if (user.GetCurrentMP() < skill.mpCost) {
            Debug.Log($"{user.GetCharacterName()} does not have enough MP to use {skill.skillName}.");
            return false;
        }
        user.UseMP(skill.mpCost); // Deduct MP cost
        switch (skill.effectType) {
            case SkillEffectType.Damage:
                int damage = CalculateDamage(user, skill);
                target.TakeDamage(damage);
                Debug.Log($"{user.GetCharacterName()} used {skill.skillName} on {target.GetCharacterName()} for {damage} damage.");
                break;
            case SkillEffectType.Heal:
                int healAmount = CalculateHeal(user, skill);
                target.Heal(healAmount);
                Debug.Log($"{user.GetCharacterName()} used {skill.skillName} on {target.GetCharacterName()} for {healAmount} healing.");
                break;
        }
        return true;
    }

    private static int CalculateDamage(Character user, SkillData skill) {
        return skill.damageType switch {
            DamageType.Physical => CalculatePhyicalDamage(user, skill),
            DamageType.Magical => CalculateMagicalDamage(user, skill),
            _ => 0,
        };
    }

    private static int CalculatePhyicalDamage(Character user, SkillData skill) {
        // Implement physical damage calculation logic here
        return skill.power * user.GetAttackPower();
    }

    private static int CalculateMagicalDamage(Character user, SkillData skill) {
        // Implement magical damage calculation logic here
        return skill.power * user.GetMagicPower();
    }

    private static int CalculateHeal(Character user, SkillData skill) {
        // Implement heal calculation logic here
        return skill.power * user.GetMagicPower();
    }
}
