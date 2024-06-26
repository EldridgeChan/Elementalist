using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarBehave : MonoBehaviour
{
    [SerializeField]
    private TMP_Text barNumberTxt;
    [SerializeField]
    private RectTransform barGaugeRect;

    public void setBar(float curr, float max)
    {
        barNumberTxt.text = (int)curr + " / " + (int)max;
        barGaugeRect.localScale = new Vector3(Mathf.Clamp01(curr / max), 1.0f, 1.0f);
    }
}
