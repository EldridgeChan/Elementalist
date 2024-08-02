using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameScripableObject gameScriptObj;
    public GameScripableObject GameScriptObj { get { return gameScriptObj; } }
    [SerializeField]
    private SpellScripableObject spellScriptObj;
    public SpellScripableObject SpellScriptObj { get { return spellScriptObj; } }
    [SerializeField]
    private LoadSceneManager loadSceneMan;
    public LoadSceneManager LoadSceneMan { get { return loadSceneMan; } }
    [SerializeField]
    private Button leaderboardBtn;

    private List<bool> knowSpells = new List<bool>();
    public List<bool> KnowSpells { get { return knowSpells; } }
    private GameController gameCon = null;
    public GameController GameCon { get { return gameCon; } set { if (!gameCon) { gameCon = value; } else { Debug.Log("ERROR: GameCon Reinit!"); } } }

    [HideInInspector]
    public int currScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (!loadSceneMan) { loadSceneMan = GetComponent<LoadSceneManager>(); }

        for (int i = 0; i < SpellScriptObj.spells.Length; i++)
        {
            knowSpells.Add(PlayerPrefs.GetInt("SpellItem" + i, 0) > 0);
        }
    }

    void Start()
    {
        if (leaderboardBtn)
        {
            leaderboardBtn.interactable = false;
        }
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully started LootLocker session");
                if (leaderboardBtn)
                {
                    leaderboardBtn.interactable = true;
                }
            }
            else
            {
                Debug.Log("ERROR starting LootLocker session");
            }
        });    
        
    }

    public void saveKnownSpells()
    {
        for (int i = 0; i < KnowSpells.Count; i++)
        {
            PlayerPrefs.SetInt("SpellItem" + i, KnowSpells[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
}
