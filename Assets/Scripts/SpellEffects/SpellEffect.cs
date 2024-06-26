using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellEffect
{
    public SpellEffectType effectType = SpellEffectType.Damage;
    public SpiritType element = SpiritType.None;
    public float damage = 0.0f;
    public int duration = 0;
    public int number = 0;

    public void applyEffect()
    {
        switch (effectType)
        {
            case SpellEffectType.Damage:
                damageEffect();
                break;
            case SpellEffectType.DamageOverTime:
                damageOverTimeEffect();
                break;
            case SpellEffectType.SpawnSpirits:
                spawnSpiritsEffect();
                break;
            case SpellEffectType.Heal:
                healEffect();
                break;
            case SpellEffectType.HealOverTime:
                healOverTimeEffect();
                break;
            case SpellEffectType.ChikyuuHakaiBakudan:
                chikyuHakaiBakudanEffect();
                break;
            default:
                Debug.Log("ERROR: Undefine Effect Type!!");
                break;
        }
    }

    private void damageEffect()
    {
        GameManager.Instance.GameCon.damageEnemy(damage, element);
    }

    private void damageOverTimeEffect()
    {
        GameManager.Instance.GameCon.addOverTimeEffect(new OverTimeEffect(damage, duration, false));
    }

    private void spawnSpiritsEffect()
    {
        GameManager.Instance.GameCon.spawnSpirits(number);
    }

    private void healEffect()
    {
        GameManager.Instance.GameCon.healPlayer(damage);
    }

    private void healOverTimeEffect()
    {
        GameManager.Instance.GameCon.addOverTimeEffect(new OverTimeEffect(damage, duration, true));
    }

    private void chikyuHakaiBakudanEffect()
    {
        GameManager.Instance.GameCon.chikyuHakaiBakudanEffect();
    }
}
