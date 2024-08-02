using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBallBehave : MonoBehaviour
{
    private bool inCastControl = false;
    private Vector2 ballStartPos = Vector2.zero;
    private Vector2 mouseStartPos = Vector2.zero;
    private int castDir = 0;
    private int castDirChangeCounter = 0;

    public void startCastControl()
    {
        ballStartPos = transform.position;
        inCastControl = true;
        mouseStartPos = InputManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void endCastControl()
    {
        inCastControl = false;
        transform.position = ballStartPos;
        castDir = 0;
        castDirChangeCounter = 0;
    }

    private void Update()
    {
        if (inCastControl)
        {
            castControl();
        }
    }

    private void castControl()
    {
        Vector2 mousePos = InputManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Clamp(mousePos.x - mouseStartPos.x, -GameManager.Instance.GameScriptObj.CastMoveMaxXPos, GameManager.Instance.GameScriptObj.CastMoveMaxXPos), Mathf.Clamp(mousePos.y - mouseStartPos.y, 0.0f, float.MaxValue)) + ballStartPos;
        hasChangeCastDirection(mousePos.x);
    }

    private void hasChangeCastDirection(float mouseXPos)
    {
        if (mouseXPos > GameManager.Instance.GameScriptObj.CastDirChangeBuffer || mouseXPos < -GameManager.Instance.GameScriptObj.CastDirChangeBuffer)
        {
            int tempDir = castDir;
            castDir = mouseXPos > 0 ? 1 : -1;
            if (tempDir != castDir)
            {
                castDirChangeCounter++;
                checkRelease();
            }
        }
    }

    private void checkRelease()
    {
        if (castDirChangeCounter > GameManager.Instance.GameScriptObj.CastReleaseNumber)
        {
            GameManager.Instance.GameCon.releaseCollected();
            endCastControl();
        }
    }

    public void castAction()
    {
        if (!inCastControl) { return; }
        if (transform.position.y >= GameManager.Instance.GameScriptObj.CastMinYPos)
        {
            GameManager.Instance.GameCon.castCollected();
        }
        endCastControl();
    }
}
