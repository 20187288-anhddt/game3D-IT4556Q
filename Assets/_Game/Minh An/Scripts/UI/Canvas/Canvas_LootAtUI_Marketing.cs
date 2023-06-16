using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Canvas_LootAtUI_Marketing : MonoBehaviour
{
    public Button Btn_Click;
    private int CountClick = 0;
    public void Awake()
    {
        Btn_Click.onClick.AddListener(Click);
    }
    public void Click()
    {
        if(CountClick == 0)
        {
            CountClick++;
            UI_Manager.Instance.DeactiveLookAllUI();
        }
        else
        {
            UI_Manager.Instance.ActiveLookAllUI();
            CountClick--;
        }
    }
}
