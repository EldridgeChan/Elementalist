using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public string name;
    public string description;
    public SpiritType[] cost;
    public SpellEffect[] effects;
    public VisualEffect visualEffect = VisualEffect.none;

    public void applyEffects()
    {
        GameManager.Instance.GameCon.showSpellText(name);
        foreach (SpellEffect effect in effects)
        {
            effect.applyEffect();
        }
    }
}
