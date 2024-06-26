using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBehave : MonoBehaviour
{
    [SerializeField]
    private SpiritType element = SpiritType.Fire;
    public SpiritType Element { get { return element; } }

    private Vector2 spawnPos = Vector2.zero;
    private Vector2 enterPos = Vector2.zero;
    private float rotateTimer = 0.0f;
    private bool clockWise = false;
    private bool collected = false;
    private bool entering = true;
    private List<Vector3> lineRendPoints = new List<Vector3>();

    private Transform castTran;
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Collider2D colid;
    [SerializeField]
    private LineRenderer lineRend;

    private void Start()
    {
        if (!rig) { rig = GetComponent<Rigidbody2D>(); }
        if (!colid) { colid = GetComponent<Collider2D>(); }
        if (!lineRend) { lineRend = GetComponent<LineRenderer>(); }

        spawnPos = transform.position;
        enterPos = new Vector2(Random.Range(GameManager.Instance.GameScriptObj.SpiritEnterMin.x, GameManager.Instance.GameScriptObj.SpiritEnterMax.x), Random.Range(GameManager.Instance.GameScriptObj.SpiritEnterMin.y, GameManager.Instance.GameScriptObj.SpiritEnterMax.y));
        clockWise = Random.Range(0, 2) == 0;
        rig.rotation = Random.Range(0.0f, 360.0f);
        rotateTimer = GameManager.Instance.GameScriptObj.SpiritEnterTimer;
    }

    private void Update()
    {
        moveTailHead();
    }

    private void FixedUpdate()
    {
        if (collected)
        {
            rig.rotation -= Time.fixedDeltaTime * GameManager.Instance.GameScriptObj.SpiritCollectRotateSpeed;
            rotateTimer = Mathf.Clamp(rotateTimer - Time.fixedDeltaTime, 0.0f, GameManager.Instance.GameScriptObj.SpiritCollectTime);
            rig.position = Vector3.Lerp(rig.position, castTran.position + new Vector3(Mathf.Cos(rig.rotation * Mathf.Deg2Rad), Mathf.Sin(rig.rotation * Mathf.Deg2Rad)) * GameManager.Instance.GameScriptObj.SpiritRotateDistance, (GameManager.Instance.GameScriptObj.SpiritCollectTime - rotateTimer) / GameManager.Instance.GameScriptObj.SpiritCollectTime) ;
        }
        else if (!entering)
        {
            rig.MoveRotation(rig.rotation + GameManager.Instance.GameScriptObj.SpiritRotateSpeed * Time.fixedDeltaTime * (clockWise ? -1.0f : 1.0f));
            rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x + Mathf.Cos(rig.rotation * Mathf.Deg2Rad) * Time.fixedDeltaTime * GameManager.Instance.GameScriptObj.SpiritAcceleration, -GameManager.Instance.GameScriptObj.SpiritMaxSpeed, GameManager.Instance.GameScriptObj.SpiritMaxSpeed), Mathf.Clamp(rig.velocity.y + Mathf.Sin(rig.rotation * Mathf.Deg2Rad) * Time.fixedDeltaTime * GameManager.Instance.GameScriptObj.SpiritAcceleration, -GameManager.Instance.GameScriptObj.SpiritMaxSpeed, GameManager.Instance.GameScriptObj.SpiritMaxSpeed));
            if (rotateTimer > 0.0f)
            {
                rotateTimer -= Time.fixedDeltaTime;
            }
            else
            {
                rotateTimer = Random.Range(GameManager.Instance.GameScriptObj.SpiritMinTimer, GameManager.Instance.GameScriptObj.SpiritMaxTimer);
                clockWise = !clockWise;
            }
        }
        else
        {
            rotateTimer -= Time.fixedDeltaTime;
            rig.position = Vector2.Lerp(spawnPos, enterPos, Mathf.Clamp01((GameManager.Instance.GameScriptObj.SpiritEnterTimer - rotateTimer) / GameManager.Instance.GameScriptObj.SpiritEnterTimer));
            if (rotateTimer <= 0.0f)
            {
                entering = false;
                initTail();
                release();
            }
        }
    }

    public void collect(Transform castTran)
    {
        this.castTran = castTran;
        colid.enabled = false;
        collected = true;
        rotateTimer = GameManager.Instance.GameScriptObj.SpiritCollectTime;
        GameManager.Instance.GameCon.addCollected(this);
        
    }

    public void release()
    {
        colid.enabled = true;
        collected = false;
        rotateTimer = 0.0f;
    }

    private void drawTail()
    {
        lineRend.positionCount = lineRendPoints.Count;
        for (int i = 1; i < lineRendPoints.Count; i++)
        {
            lineRend.SetPosition(i, lineRendPoints[i]);
        }
    }

    private void addTailPoint()
    {
        lineRendPoints.Insert(1, Vector3.Lerp(transform.position, lineRendPoints[1], GameManager.Instance.GameScriptObj.SpiritTailPointOffSetT));
        Invoke("expireTailEnd", GameManager.Instance.GameScriptObj.SpiritTailPointExpireTime);
    }

    private void expireTailEnd()
    {
        lineRendPoints.RemoveAt(lineRendPoints.Count - 1);
    }

    private void moveTailHead()
    {
        if (entering)
        {
            return;
        }
        lineRendPoints[0] = transform.position;
        lineRend.SetPosition(0, lineRendPoints[0]);
        if (Vector3.Distance(transform.position, lineRendPoints[1]) > GameManager.Instance.GameScriptObj.SpiritTailPointDistance)
        {
            addTailPoint();
            drawTail();
        }
    }

    private void initTail()
    {
        lineRend.enabled = true;
        lineRendPoints.Add(transform.position);
        lineRendPoints.Add(transform.position);
        drawTail();
    }
}
