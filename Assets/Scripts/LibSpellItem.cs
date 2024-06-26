using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LibSpellItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameTxt;
    [SerializeField]
    private TMP_Text elementTxt;
    [SerializeField]
    private TMP_Text descriptionTxt;
    [SerializeField]
    private Image[] costArr;

    public void init(int index, Spell spell)
    {
        if (spell != null)
        {
            nameTxt.text = (index + 1) + ". " + spell.name;
            elementTxt.text = "Element: " + spell.effects[0].element;
            descriptionTxt.text = spell.description;
            for (int i = 0; i < costArr.Length; i++)
            {
                if (i < spell.cost.Length)
                {
                    costArr[i].color = GameManager.Instance.GameScriptObj.elemntColors[(int)spell.cost[i] + 1];
                }
                costArr[i].enabled = i < spell.cost.Length;
            }
        }
        else
        {
            nameTxt.text = (index + 1) + ". ?????";
            elementTxt.text = "Element: ??????";
            descriptionTxt.text = "?????????";
            for (int i = 0; i < costArr.Length; i++)
            {
                costArr[i].enabled = false;
            }
        }
    }
}