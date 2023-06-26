using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Canvas_Joystick : Singleton<Canvas_Joystick>
{
    private static float LIMIT_DISTANCE_TOUTCH = 120;
    [SerializeField] private Transform Trans_Touch;
    [SerializeField] private Transform Trans_BG;
    [SerializeField] private GameObject JoyStick;

    private Vector3 Diraction = Vector3.zero;
    private Vector3 Position_Mouse;
    public bool isStopJoysick;

    public override void Awake()
    {
        base.Awake();
        OnInIt();
    }
    public void OnInIt()
    {
        isStopJoysick = false;
    }
    private void Update()
    {
        if (isStopJoysick)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Position_Mouse = Input.mousePosition;
            Trans_BG.position = Position_Mouse;
            Trans_Touch.localPosition = Vector3.zero;
            JoyStick.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            Position_Mouse = Input.mousePosition;
            Trans_Touch.transform.position = Position_Mouse;

            Diraction = Trans_Touch.transform.localPosition.normalized;

            if (Vector3.Distance(Vector3.zero, Trans_Touch.transform.localPosition) >= LIMIT_DISTANCE_TOUTCH)
            {
                Diraction.Normalize();
                Trans_Touch.transform.localPosition = Diraction * LIMIT_DISTANCE_TOUTCH;
            }
            else
            {
                Trans_Touch.transform.position = Position_Mouse;
            }
            EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
        }
        if (Input.GetMouseButtonUp(0))
        {
            Trans_Touch.transform.localPosition = Vector3.zero;
            JoyStick.SetActive(false);
            EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        }
    }
    public Vector3 Get_Diraction()
    {
        Vector3 Diraction_ = new Vector3(Diraction.x, 0, Diraction.y);
        return Diraction_.normalized;
    }
    public bool isOpenJoystick()
    {
        return JoyStick.activeSelf;
    }
}
