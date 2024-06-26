using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;

    private void Start()
    {
        if (mainCam == null) { mainCam = Camera.main; }
    }

    void Update()
    {
        inputLoop();
    }

    private void inputLoop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCam == null) { mainCam = Camera.main; }
            tryRaycastMouse();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.GameCon)
            {
                GameManager.Instance.GameCon.CastBall.castAction();
            }
        }
    }

    private void tryRaycastMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Spirit"))
            {
                hit.collider.gameObject.GetComponent<SpiritBehave>().collect(GameManager.Instance.GameCon.CastBall.transform);
            }
            else if (hit.collider.gameObject.CompareTag("Casting"))
            {
                GameManager.Instance.GameCon.CastBall.startCastControl();
            }
        }
    }
}
