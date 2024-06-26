using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;
    [SerializeField]
    TMPro.TMP_Text text;

    public void init(SpiritType element, float amount, Vector2 pos)
    {
        Destroy(gameObject, 2.0f);
        rect.anchoredPosition = pos;
        text.text = amount + "";
        switch (element)
        {
            case SpiritType.None:
                text.faceColor = GameManager.Instance.GameScriptObj.elemntColors[0];
                break;
            case SpiritType.Fire:
                text.faceColor = GameManager.Instance.GameScriptObj.elemntColors[1];
                break;
            case SpiritType.Air:
                text.faceColor = GameManager.Instance.GameScriptObj.elemntColors[2];
                break;
            case SpiritType.Earth:
                text.faceColor = GameManager.Instance.GameScriptObj.elemntColors[3];
                break;
            case SpiritType.Water:
                text.faceColor = GameManager.Instance.GameScriptObj.elemntColors[4];
                break;
            default:
                Debug.Log("ERROR: Undefined Element");
                break;
        }
    }
}
