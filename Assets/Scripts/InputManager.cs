using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private Camera mainCam;
    public Camera MainCam { 
        get { 
            if (mainCam == null)
            {
                mainCam = Camera.main;
            }
            return mainCam; 
        } 
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    void Update()
    {
        inputLoop();
    }

    private void inputLoop()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        RaycastHit2D hit = Physics2D.Raycast(MainCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
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
