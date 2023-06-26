using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Settings : UI_Canvas
{
    [SerializeField] private Button btn_Close;
    [Header("Button Haptics")]
    [SerializeField] private Button btn_Haptics;
    [SerializeField] private Text txt_Haptics;
    [Header("Button FixedJoystick")]
    [SerializeField] private Button btn_FixedJoystick;
    [SerializeField] private Text txt_FixedJoystick;


    private static string nameHaptics = "Haptics: ";
    private static string nameJoystick = "Fixed Joystick: ";

    private int indexClick_Haptics = 0;
    private int indexClick_Joystick = 0;
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        //indexClick_Haptics = 1;
        //indexClick_Joystick = 1;

        //CheckData_Haptics(indexClick_Haptics);
        //CheckData_FixedJoystick(indexClick_Joystick);

        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        //btn_Haptics.onClick.AddListener(() => { Click_Haptics(); });
        //btn_FixedJoystick.onClick.AddListener(() => { Click_FixedJoystick(); });
    }
    public void Click_Haptics()
    {
        if(indexClick_Haptics == 0)
        {
            indexClick_Haptics++;
            CheckData_Haptics(indexClick_Haptics);
        }
        else
        {
            indexClick_Haptics--;
            CheckData_Haptics(indexClick_Haptics);
        }
    }
    public void Click_FixedJoystick()
    {
        if (indexClick_Joystick == 0)
        {
            indexClick_Joystick++;
            CheckData_FixedJoystick(indexClick_Joystick);
        }
        else
        {
            indexClick_Joystick--;
            CheckData_FixedJoystick(indexClick_Joystick);
        }
    }
    private void CheckData_Haptics(int valueData)
    {
        if (valueData == 0)
        {
            txt_Haptics.text = nameHaptics + "Off";
        }
        else
        {
            txt_Haptics.text = nameHaptics + "On";
        }
    }
    private void CheckData_FixedJoystick(int valueData)
    {
        if (indexClick_Joystick == 0)
        {
            txt_FixedJoystick.text = nameJoystick + "Off";
        }
        else
        {
            txt_FixedJoystick.text = nameJoystick + "On";
        }
    }
}
