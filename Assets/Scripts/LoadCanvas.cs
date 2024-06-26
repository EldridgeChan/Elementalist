using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LoadCanvas : MonoBehaviour
{
    private int leaderboardID = 15424;
    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text placeholder;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject mainParent;
    [SerializeField]
    private CreditBehave creditBehave;

    private void Start()
    {
        if (scoreText)
        {
            scoreText.text = "Your Score: Level " + GameManager.Instance.currScore;
        }
    }

    public void loadScene(int scene)
    {
        GameManager.Instance.LoadSceneMan.loadScene((SceneList)scene);
    }

    public void submitScore()
    {
        submitScoreToLeaderBoard(GameManager.Instance.currScore);
    }

    public void submitScoreToLeaderBoard(int score)
    {
        if (playerName.text != "")
        {
            LootLockerSDKManager.SetPlayerName(playerName.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully Set Player Name");
                }
                else
                {
                    Debug.Log("ERROR: Cannot Set Player Name");
                }
            });

            LootLockerSDKManager.SubmitScore(playerName.text, score, leaderboardID + "", (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Score Submit Successful");
                }
                else
                {
                    Debug.Log("Failed: " + response.Error);
                }
            });
            loadScene(0);
        }
        else
        {
            placeholder.text = "Please " + placeholder.text;
        }
    }

    public void openCredit(bool open)
    {
        mainParent.gameObject.SetActive(!open);
        creditBehave.startCredit(open);
    }
}
