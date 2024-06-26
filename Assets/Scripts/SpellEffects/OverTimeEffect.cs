using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeEffect
{
    private float hpChange;
    private int timeInterval;
    private bool toSelf;
    private float timer = 1.0f;

    public OverTimeEffect(float hpChange, int timeInterval, bool toSelf)
    {
        this.hpChange = hpChange;
        this.timeInterval = timeInterval;
        this.toSelf = toSelf;
    }

    public void countDown(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0.0f)
        {
            timer = 1.0f;
            applyEffect();
            timeInterval--;
            if (timeInterval < 0)
            {
                GameManager.Instance.GameCon.removeOverTimeEffect(this);
            }
        }
    }

    private void applyEffect()
    {
        if (toSelf)
        {
            GameManager.Instance.GameCon.healPlayer(hpChange);
        }
        else
        {
            GameManager.Instance.GameCon.damageEnemy(hpChange);
        }
    }
}
