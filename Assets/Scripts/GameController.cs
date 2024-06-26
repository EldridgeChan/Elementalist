using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private BarBehave playerHealthBar;
    [SerializeField]
    private TMP_Text levelTxt;
    [SerializeField]
    private TMP_Text spellTxt;
    [SerializeField]
    private Animator spellAnimt;
    [SerializeField]
    private GameObject healTextPrefab;
    [SerializeField]
    private RectTransform healTextParent;
    [SerializeField]
    private GameObject damageTextPrefab;
    [SerializeField]
    private RectTransform damageTextParent;
    [SerializeField]
    private RectTransform enemyCanvasRect;
    [SerializeField]
    private Animator boomAnmt;
    [SerializeField]
    private Image boomImage;
    [SerializeField]
    private Canvas gameCanvas;
    [SerializeField]
    private CastBallBehave castBall;
    public CastBallBehave CastBall { get { return castBall; } }

    private List<SpiritBehave> collectedSpirits = new List<SpiritBehave>();
    private List<OverTimeEffect> overTimeEffects = new List<OverTimeEffect>();

    private Enemy currEnemy = null;
    private float currPlayerHealth;
    private int levelNumer = 0;
    private bool chikyuHakaiBakudan = false;
    public int LevelNumer { get { return levelNumer; } }

    private void Start()
    {
        GameManager.Instance.GameCon = this;
        currPlayerHealth = GameManager.Instance.GameScriptObj.PlayerDefaultHealth;
        updateHealthBar();
        spawnSpirits(GameManager.Instance.GameScriptObj.SpiritStartSpawnNumber);
        Invoke("spiritsNaturalSpawn", GameManager.Instance.GameScriptObj.SpiritNaturalSpawnTimer);
        startNextLevel();
    }

    private void Update()
    {
        if (overTimeEffects.Count > 0)
        {
            for (int i = overTimeEffects.Count - 1; i >= 0; i--)
            {
                overTimeEffects[i].countDown(Time.deltaTime);
            }
        }
    }

    public void addOverTimeEffect(OverTimeEffect effect)
    {
        overTimeEffects.Add(effect);
    }

    public void removeOverTimeEffect(OverTimeEffect effect)
    {
        overTimeEffects.Remove(effect);
    }

    public void addCollected(SpiritBehave spirit)
    {
        collectedSpirits.Add(spirit);
    }

    public void releaseCollected()
    {
        for (int i = collectedSpirits.Count - 1; i >= 0; i--)
        {
            collectedSpirits[i].release();
            collectedSpirits.RemoveAt(i);
        }
    }

    public void castCollected()
    {
        if (GameManager.Instance.SpellScriptObj.castSpell(sortCollected()))
        {

        }
        for (int i = collectedSpirits.Count - 1; i >= 0; i--)
        {
            Destroy(collectedSpirits[i].gameObject);
        }
        collectedSpirits.Clear();
    }

    private int[] sortCollected()
    {
        int[] spirits = new int[] { 0, 0, 0, 0 };
        for (int i = 0; i < collectedSpirits.Count; i++)
        {
            spirits[(int)collectedSpirits[i].Element]++;
        }
        return spirits;
    }

    private void spiritsNaturalSpawn()
    {
        spawnSpirits(GameManager.Instance.GameScriptObj.SpiritNaturalSpawnNumber);
        Invoke("spiritsNaturalSpawn", GameManager.Instance.GameScriptObj.SpiritNaturalSpawnTimer);
    }

    public void spawnSpirits(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Transform tran = Instantiate(GameManager.Instance.GameScriptObj.SpiritTypes[Random.Range(0, 4)]).transform;
            tran.position = new Vector2(Random.Range(-GameManager.Instance.GameScriptObj.SpiritSpawnX, GameManager.Instance.GameScriptObj.SpiritSpawnX), GameManager.Instance.GameScriptObj.SpiritSpawnY);
        }
    }

    public void attacked()
    {
        currPlayerHealth = Mathf.Clamp(currPlayerHealth - GameManager.Instance.GameScriptObj.levelDamage(), 0.0f, GameManager.Instance.GameScriptObj.PlayerDefaultHealth);
        updateHealthBar();
        if (currPlayerHealth <= 0.0f)
        {
            playerDie();
        }
    }

    private void playerDie()
    {
        if (!chikyuHakaiBakudan)
        {
            CancelInvoke();
            GameManager.Instance.saveKnownSpells();
            GameManager.Instance.LoadSceneMan.loadScene(SceneList.EndGame);
        }
    }

    public void startNextLevel()
    {
        levelNumer++;
        GameManager.Instance.currScore = LevelNumer;
        //Debug.Log("Damage: " + GameManager.Instance.GameScriptObj.levelDamage() + " Health: " + GameManager.Instance.GameScriptObj.levelHealth() + " Speed: " + GameManager.Instance.GameScriptObj.levelSpeed());
        Invoke("spawnEnemy", GameManager.Instance.GameScriptObj.EnemySpawnTimer);
    }

    private void spawnEnemy()
    {
        levelTxt.text = "Level: " + LevelNumer;
        currEnemy = Instantiate(GameManager.Instance.GameScriptObj.EnemyTypes[Random.Range(0, 4)], enemyCanvasRect).GetComponent<Enemy>();
    }

    public void damageEnemy(float damage, SpiritType element = SpiritType.None)
    {
        if (currEnemy != null)
        {
            currEnemy.attacked(damage, element);
        }
    }

    public void showDamageText(float damage, SpiritType element)
    {
        DamageText damageText = Instantiate(damageTextPrefab, damageTextParent).GetComponent<DamageText>();
        float distance = Random.Range(0.0f, GameManager.Instance.GameScriptObj.damageTextSpawnRadius);
        float angle = Random.Range(0.0f, 360.0f);
        damageText.init(element, damage, new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * distance);
    }

    public void healPlayer(float amount)
    {
        currPlayerHealth = Mathf.Clamp(currPlayerHealth + amount, 0.0f, GameManager.Instance.GameScriptObj.PlayerDefaultHealth);
        showHealText(amount);
        updateHealthBar();
    }

    private void showHealText(float amount)
    {
        TMP_Text healtxt = Instantiate(healTextPrefab, healTextParent).GetComponent<TMP_Text>();
        healtxt.text = "+" + (int)amount;

    }

    private void updateHealthBar()
    {
        playerHealthBar.setBar(currPlayerHealth, GameManager.Instance.GameScriptObj.PlayerDefaultHealth);
    }

    public void showSpellText(string spellName)
    {
        spellTxt.text = spellName;
        spellAnimt.SetTrigger("Show");
    }

    public void chikyuHakaiBakudanEffect()
    {
        boomImage.enabled = true;
        chikyuHakaiBakudan = true;
        Invoke("boomAnime", 1.0f);
        Invoke("loadBoom", GameManager.Instance.GameScriptObj.BakudanLoadTime);
    }
    private void boomAnime()
    {
        boomAnmt.SetTrigger("Boom");
    }

    private void loadBoom()
    {
        GameManager.Instance.LoadSceneMan.loadScene(SceneList.ChikyuuHakaiBakudan);
    }
}
