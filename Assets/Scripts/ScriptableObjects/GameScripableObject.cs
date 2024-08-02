using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObject", menuName = "ScriptableObjects/GameScriptableObject")]
public class GameScripableObject : ScriptableObject
{
    [Header("Spirit")]
    public float SpiritRotateSpeed = 20.0f;
    public float SpiritMaxSpeed = 5.0f;
    public float SpiritAcceleration = 2.0f;
    public float SpiritMinTimer = 3.0f;
    public float SpiritMaxTimer = 6.0f;
    public float SpiritCollectRotateSpeed = 5.0f;
    public float SpiritCollectTime = 2.0f;
    public float SpiritRotateDistance = 1.0f;
    public float SpiritEnterTimer = 2.0f;
    public Vector2 SpiritEnterMax = Vector2.zero;
    public Vector2 SpiritEnterMin = Vector2.zero;
    public float SpiritSpawnY = 10.0f;
    public float SpiritSpawnX = 10.0f;
    public GameObject[] SpiritTypes;
    public float SpiritNaturalSpawnTimer = 5.0f;
    public int SpiritNaturalSpawnNumber = 2;
    public int SpiritStartSpawnNumber = 8;
    public float SpiritTailPointDistance = 0.2f;
    public float SpiritTailPointOffSetT = 0.05f;
    public float SpiritTailPointExpireTime = 1.0f;
    public float SpiritWallOffset = 0.5f;
    public float SpiritHandWallOffset = 1.0f;

    [Header("Enemy")]
    public float EnemyDefaultHealth = 100.0f;
    public float EnemyHealthMul = 1.001f;
    public float EnemyDefaultDamage = 10.0f;
    public float EnemyDamageMul = 1.01f;
    public float EnemyDefaultSpeed = 2.0f;
    public float EnemySpeedMul = 0.998f;
    public float WeakElementMul = 1.5f;
    public float StrongElementMul = 0.5f;
    public float EnemySpawnTimer = 3.0f;
    public float damageTextSpawnRadius = 10.0f;
    public Color[] elemntColors;
    public GameObject[] EnemyTypes;

    [Header("Player")]
    public float PlayerDefaultHealth = 100.0f;
    public float CastMinYPos = 0.0f; //release above this to cast spell
    public float CastMoveMaxXPos = 1.0f; //the max x pos the ball can go
    public float CastDirChangeBuffer = 0.2f; //move cast more than this to count as change dir
    public int CastReleaseNumber = 5;

    [Header("Leaderboard")]
    public int scorePerPage = 8;
    public float leaderboardDistance = 150.0f;

    [Header("UI")]
    public float creditStartPos = -600.0f;
    public float creditEndPos = 2480.0f;
    public float creditScrollSpeed = 5.0f;
    public float creditEndTimer = 5.0f;
    public int spellPerPage = 4;
    public float spellDistance = 200.0f;
    public float BakudanLoadTime = 3.0f;
    public float BakudanTextShowTimerFirst = 2.0f;
    public float BakudanTextShowTimerSecond = 4.0f;

    public float levelHealth()
    {
        float health = EnemyDefaultHealth;
        for (int i = 0; i < GameManager.Instance.GameCon.LevelNumer - 1; i++)
        {
            health *= EnemyHealthMul;
        }
        return health;
    }

    public float levelDamage()
    {
        float damage = EnemyDefaultDamage;
        for (int i = 0; i < GameManager.Instance.GameCon.LevelNumer - 1; i++)
        {
            damage *= EnemyDamageMul;
        }
        return damage;
    }

    public float levelSpeed()
    {
        float speed = EnemyDefaultSpeed;
        for (int i = 0; i < GameManager.Instance.GameCon.LevelNumer - 1; i++)
        {
            speed *= EnemySpeedMul;
        }
        return speed;
    }
}
