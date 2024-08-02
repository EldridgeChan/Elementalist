using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsPosition : MonoBehaviour
{
    [SerializeField]
    private RectTransform gameCanvasTrans;
    [SerializeField]
    private Transform wallNTrans;
    [SerializeField]
    private Transform wallETrans;
    [SerializeField]
    private Transform wallSTrans;
    [SerializeField]
    private Transform wallWTrans;
    [SerializeField]
    private Transform wallHandTrans;

    void Start()
    {
        SetWallsPostion();
    }

    public void SetWallsPostion()
    {
        Vector3[] cornerPositions = new Vector3[4];
        gameCanvasTrans.GetWorldCorners(cornerPositions);
        wallWTrans.position = new Vector2(cornerPositions[0].x - GameManager.Instance.GameScriptObj.SpiritWallOffset, 0.0f);
        wallNTrans.position = new Vector2(0.0f, cornerPositions[1].y + GameManager.Instance.GameScriptObj.SpiritWallOffset);
        wallETrans.position = new Vector2(cornerPositions[2].x + GameManager.Instance.GameScriptObj.SpiritWallOffset, 0.0f);
        wallSTrans.position = new Vector2(0.0f, cornerPositions[3].y);
        wallHandTrans.position = new Vector2(0.0f, cornerPositions[0].y + GameManager.Instance.GameScriptObj.SpiritHandWallOffset);
    }
}
