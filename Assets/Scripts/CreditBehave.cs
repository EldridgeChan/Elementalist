using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBehave : MonoBehaviour
{
    [SerializeField]
    private RectTransform creditRect;
    [SerializeField]
    private LoadCanvas loadCanvas;
    [SerializeField]
    private GameObject creditParent;

    private bool isScroll;
    private float backTimer = 0.0f;

    private void Update()
    {
        if (isScroll)
        {
            creditRect.anchoredPosition = new Vector2(0.0f, Mathf.Clamp(creditRect.anchoredPosition.y + Time.deltaTime * GameManager.Instance.GameScriptObj.creditScrollSpeed, GameManager.Instance.GameScriptObj.creditStartPos, GameManager.Instance.GameScriptObj.creditEndPos));
            if (creditRect.anchoredPosition.y >= GameManager.Instance.GameScriptObj.creditEndPos)
            {
                backTimer -= Time.deltaTime;
                if (backTimer < 0.0f)
                {
                    loadCanvas.openCredit(false);
                }
            }
        }
    }

    public void startCredit(bool start)
    {
        if (start)
        {
            creditParent.SetActive(true);
            isScroll = true;
            backTimer = GameManager.Instance.GameScriptObj.creditEndTimer;
            creditRect.anchoredPosition = Vector2.up * GameManager.Instance.GameScriptObj.creditStartPos;
        }
        else
        {
            creditParent.SetActive(false);
            isScroll = false;
        }
    }
}
