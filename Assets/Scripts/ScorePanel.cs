using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private TMP_Text queueTxt;
    [SerializeField]
    private TMP_Text nameTxt;
    [SerializeField]
    private TMP_Text scoreTxt;

    public void init(int rank, string name, int score)
    {
        rect.anchoredPosition = new Vector2(0.0f, -(float)((rank - 1) % GameManager.Instance.GameScriptObj.scorePerPage * GameManager.Instance.GameScriptObj.leaderboardDistance));
        queueTxt.text = "#" + rank;
        nameTxt.text = name;
        scoreTxt.text = "Score: Level " + score;
    }
}
