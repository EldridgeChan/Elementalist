using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spellItems;
    [SerializeField]
    private LibSpellItem[] libSpellItems;
    [SerializeField]
    private Button upPageBtn;
    [SerializeField]
    private Button downPageBtn;

    private int pageNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        pageChange(0);
    }

    public void showSpells()
    {
        for (int i = 0; i < spellItems.Length; i++)
        {
            if (i + pageNumber * GameManager.Instance.GameScriptObj.spellPerPage < GameManager.Instance.SpellScriptObj.spells.Length)
            {
                spellItems[i].SetActive(true);
                libSpellItems[i].init(i + pageNumber * GameManager.Instance.GameScriptObj.spellPerPage, GameManager.Instance.KnowSpells[i + pageNumber * GameManager.Instance.GameScriptObj.spellPerPage] ? GameManager.Instance.SpellScriptObj.spells[i + GameManager.Instance.GameScriptObj.spellPerPage * pageNumber] : null);
            }
            else
            {
                spellItems[i].SetActive(false);
            }
        }
    }

    public void pageChange(int dir)
    {
        pageNumber += dir;
        showSpells();
        checkButton();
    }

    public void checkButton()
    {
        upPageBtn.interactable = pageNumber > 0;
        downPageBtn.interactable = (pageNumber + 1) * GameManager.Instance.GameScriptObj.spellPerPage <= GameManager.Instance.SpellScriptObj.spells.Length;
    }

    public void loadMainmenu()
    {
        GameManager.Instance.LoadSceneMan.loadScene(SceneList.MainMenu);
    }
}
