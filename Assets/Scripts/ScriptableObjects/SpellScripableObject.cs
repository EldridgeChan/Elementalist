using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellScripableObject", menuName = "ScriptableObjects/SpellScripableObject")]
public class SpellScripableObject : ScriptableObject
{
    public Spell[] spells;

    public bool castSpell(int[] spirits)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (checkCost(spells[i], spirits))
            {
                GameManager.Instance.KnowSpells[i] = true;
                GameManager.Instance.GameCon.PlayMagicVisualEffect(spells[i].visualEffect);
                spells[i].applyEffects();
                return true;
            }
        }
        GameManager.Instance.GameCon.PlayMagicVisualEffect(VisualEffect.FailCast);
        return false;
    }

    private bool checkCost(Spell spell, int[] spirits)
    {
        for (int i = 0; i < 4; i++)
        {
            if (spell.cost[i] != spirits[i])
            {
                return false;
            }
        }
        return true;
    }
}
