using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Table_Info_Bonus : UI_Child
{
    [Header("Label Bonus")]
    [SerializeField] private List<UI_TableLabelBuff> uI_TableLabelBuffs;
    [Header("Button")]
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_Watch;
    [Header("Action")]
    [SerializeField] private System.Action actionWatch;
    private void Awake()
    {
        btn_Close.onClick.AddListener(Close);
        btn_Watch.onClick.AddListener(OnClickWatch);
    }
    private void OnClickWatch()
    {
        actionWatch?.Invoke();
        actionWatch = null;
        Close();
    }
    public void Show(System.Action action, TypeBonus typeBonusShow)
    {
        actionWatch = action;
        OpenLabelBuff(typeBonusShow);
    }
    private void OpenLabelBuff(TypeBonus typeBonusShow)
    {
        foreach(UI_TableLabelBuff uI_TableLabelBuff in uI_TableLabelBuffs)
        {
            if(uI_TableLabelBuff.typeBonus == typeBonusShow)
            {
                uI_TableLabelBuff.Open();
            }
            else
            {
                uI_TableLabelBuff.Close();
            }
        }
    }
    public UI_TableLabelBuff GetI_TableLabelBuff(TypeBonus typeBonusShow)
    {
        foreach (UI_TableLabelBuff uI_TableLabelBuff in uI_TableLabelBuffs)
        {
            if (uI_TableLabelBuff.typeBonus == typeBonusShow)
            {
                return uI_TableLabelBuff;
            }
           
        }
        return null;
    }
    public override void Open()
    {
        base.Open();
        UI_Manager.Instance.GetUI(NameUI.Canvas_Bonus).canvasThis.sortingOrder = 1000;
    }
    public override void Close()
    {
        base.Close();
        UI_Manager.Instance.GetUI(NameUI.Canvas_Bonus).canvasThis.sortingOrder = -1;
    }

}
