using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Canvas_Joystick : UI_Canvas
{
    private static Canvas_Joystick instance;
    public static Canvas_Joystick Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Canvas_Joystick>();
            }
            return instance;
        }
    }
    private static float LIMIT_DISTANCE_TOUTCH = 120;
    [SerializeField] private Transform Trans_Touch;
    [SerializeField] private Transform Trans_BG;
    [SerializeField] private GameObject JoyStick;
    [SerializeField] private Animator animTuT;
    [SerializeField] private GameObject hand;
    [SerializeField] private RectTransform Rect_JoyStick;
    [SerializeField] private Vector3 Position_TuT_Mouse;
    private Vector3 Diraction = Vector3.zero;
    private Vector3 Position_Mouse;
    public bool isStopJoysick;

    private static float m_MaxTimeDeactiveTouch = 30;
    private float m_TimeDeactiveTouch = 0;

    public void Awake()
    {
        OnInIt();
    }
    //public void Start()
    //{
    //    EnventManager.AddListener(EventName.PlayJoystick.ToString(), () => { isStopJoysick = false; });
    //    EnventManager.AddListener(EventName.StopJoyStick.ToString(), () => { isStopJoysick = true; });
    //}
    public override void OnInIt()
    {
        base.OnInIt();
        isStopJoysick = false;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            m_TimeDeactiveTouch = m_MaxTimeDeactiveTouch;
            animTuT.enabled = false;
            hand.SetActive(false);
        }
        else
        {
            if (m_TimeDeactiveTouch > 0)
            {
                m_TimeDeactiveTouch -= Time.deltaTime;
            }
            else
            {
                animTuT.enabled = true;
                hand.SetActive(true);
                Rect_JoyStick.anchoredPosition3D = Position_TuT_Mouse;
                return;
            }
        }
       
        if (isStopJoysick || EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.currentSelectedGameObject.gameObject.activeInHierarchy)
                {
                    JoyStick.SetActive(false);
                    return;
                }
            }
            else
            {
                JoyStick.SetActive(false);
                return;
            }
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
            if (!JoyStick.activeInHierarchy)
            {
                JoyStick.SetActive(true);
                Position_Mouse = Input.mousePosition;
                Trans_BG.position = Position_Mouse;
                Trans_Touch.localPosition = Vector3.zero;
            }
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
