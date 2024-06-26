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
            if (correctSize(spells[i].cost.Length, spirits) && checkCost(spells[i], spirits))
            {
                GameManager.Instance.KnowSpells[i] = true;
                spells[i].applyEffects();
                return true;
            }
        }
        return false;
    }

    private bool checkCost(Spell spell, int[] spirits)
    {
        int index = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < spirits[i]; j++)
            {
                if (spell.cost[index] == (SpiritType)i)
                {
                    index++;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool correctSize(int size, int[] cost)
    {
        int checkSize = 0;
        for (int i = 0; i < 4; i++)
        {
            checkSize += cost[i];
        }
        return size == checkSize;
    }
}
