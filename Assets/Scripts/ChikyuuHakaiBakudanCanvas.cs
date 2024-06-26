using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuuHakaiBakudanCanvas : MonoBehaviour
{
    [SerializeField]
    private Image brightImage;
    [SerializeField]
    private TMPro.TMP_Text noOneTxt;
    [SerializeField]
    private Button submitBtn;
    [SerializeField]
    private Animator brightAnmt;
    [SerializeField]
    private Animator[] texts;


    private void Start()
    {
        Invoke("firstTextAppear", 2.0f);
    }

    private void firstTextAppear()
    {
        texts[0].SetTrigger("Show");
        texts[1].SetTrigger("Show");
        Invoke("secTextAppear", GameManager.Instance.GameScriptObj.BakudanTextShowTimerFirst);
    }

    private void secTextAppear()
    {
        texts[2].SetTrigger("Show");
        Invoke("thirdTextAppear", GameManager.Instance.GameScriptObj.BakudanTextShowTimerFirst);

    }

    private void thirdTextAppear()
    {
        texts[3].SetTrigger("Show");
        Invoke("brightFade", GameManager.Instance.GameScriptObj.BakudanTextShowTimerSecond);
    }

    private void brightFade()
    {
        texts[0].SetTrigger("Fade");
        texts[1].SetTrigger("Fade");
        texts[2].SetTrigger("Fade");
        texts[3].SetTrigger("Fade");
        brightAnmt.SetTrigger("Fade");
        Invoke("inactiveateBright", 3.0f);
    }

    private void inactiveateBright()
    {
        brightAnmt.gameObject.SetActive(false);
    }

    public void falseSubmit()
    {
        noOneTxt.gameObject.SetActive(true);
        submitBtn.gameObject.SetActive(false);
    }

    public void back()
    {
        GameManager.Instance.LoadSceneMan.loadScene(SceneList.MainMenu);
    }
}
