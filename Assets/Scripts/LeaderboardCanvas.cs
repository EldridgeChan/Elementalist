using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UI;

public class LeaderboardCanvas : MonoBehaviour
{
    private string leaderboardKey = "elementalistLeaderboardLive";
    private int leaderboardPage = 0;
    [SerializeField]
    private GameObject scoreItemPrefab;
    [SerializeField]
    private RectTransform leaderboardRect;
    [SerializeField]
    private Button upPageBtn;
    [SerializeField]
    private Button downPageBtn;

    public void Start()
    {
        getScores();
    }

    public void getScores()
    {
        LootLockerSDKManager.GetScoreList(leaderboardKey, GameManager.Instance.GameScriptObj.scorePerPage + 1, leaderboardPage * GameManager.Instance.GameScriptObj.scorePerPage, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully get scorelist");
                showLeaderboard(response.items);
                buttonCheck(response.items);
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
            }
        });

    }

    public void showLeaderboard(LootLockerLeaderboardMember[] leaderboardShowData)
    {
        for (int i = 0; i < leaderboardShowData.Length && i < GameManager.Instance.GameScriptObj.scorePerPage; i++)
        {
            ScorePanel temp = Instantiate(scoreItemPrefab, leaderboardRect).GetComponent<ScorePanel>();
            temp.init(leaderboardShowData[i].rank, leaderboardShowData[i].player.name, leaderboardShowData[i].score);
        }
    }

    public void pageChange(int dir)
    {
        leaderboardPage = Mathf.Clamp(leaderboardPage + dir, 0, int.MaxValue);
        getScores();
    }

    private void buttonCheck(LootLockerLeaderboardMember[] leaderboardShowData)
    {
        upPageBtn.interactable = leaderboardPage > 0;
        downPageBtn.interactable = leaderboardShowData.Length > GameManager.Instance.GameScriptObj.scorePerPage;

    }
}
