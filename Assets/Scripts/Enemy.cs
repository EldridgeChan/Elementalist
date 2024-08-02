using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private SpiritType elemental = SpiritType.None;

    private float currHealth = 100.0f;
    private float attackTimer = 0.0f;

    [SerializeField]
    private Animator amtr;
    [SerializeField]
    private BarBehave healthBar;

    private void Start()
    {
        if (amtr == null) { amtr = GetComponent<Animator>(); }
        currHealth = GameManager.Instance.GameScriptObj.levelHealth();
        attackTimer = GameManager.Instance.GameScriptObj.levelSpeed();
        healthBar.setBar(currHealth, GameManager.Instance.GameScriptObj.levelHealth());
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0.0f)
        {
            attack();
            attackTimer = GameManager.Instance.GameScriptObj.levelSpeed();
        }
    }

    private void attack()
    {
        GameManager.Instance.GameCon.attacked();
        amtr.SetTrigger("Attack");
    }

    public void attacked(float damage, SpiritType element)
    {
        currHealth = Mathf.Clamp(currHealth - damage * damageMultiplier(element), 0.0f, GameManager.Instance.GameScriptObj.levelHealth());
        GameManager.Instance.GameCon.showDamageText(damage * damageMultiplier(element), element, IsWeakToElement(element));
        healthBar.setBar(currHealth, GameManager.Instance.GameScriptObj.levelHealth());
        if (currHealth <= 0.0f)
        {
            die();
        }
    }

    private void die()
    {
        GameManager.Instance.GameCon.startNextLevel();
        Destroy(gameObject);
    }

    private float damageMultiplier(SpiritType element)
    {
        if (element == SpiritType.None || Mathf.Abs((int)element - (int)elemental) == 2)
        {
            return 1.0f;
        }
        else if (element == elemental)
        {
            return 0.0f;
        }
        else if (IsWeakToElement(element))
        {
            return GameManager.Instance.GameScriptObj.WeakElementMul;
        }
        else
        {
            return GameManager.Instance.GameScriptObj.StrongElementMul;
        }
    }

    private bool IsWeakToElement(SpiritType element)
    {
        return ((int)element + 1) % (int)SpiritType.endOfElement == (int)elemental;
    }
}
