using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Upgrades : UI_Canvas
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private UI_LabelShow uI_LabelShow_Prefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<UI_LabelShow> uI_LabelShows;
    [Header("Button")]
    [SerializeField] private Button btn_Worker;
    [SerializeField] private Button btn_Machine;
    [Header("Image")]
    [SerializeField] private Image img_BG_Worker;
    [SerializeField] private Image img_BG_Machine;
    [Header("Sprites")]
    [SerializeField] private Sprite spr_BG_On;
    [SerializeField] private Sprite spr_BG_Off;
    [Header("Text")]
    [SerializeField] private Text txt_Worker;
    [SerializeField] private Text txt_Machine;
    [Header("Color Text")]
    [SerializeField] private Color color_On;
    [SerializeField] private Color color_Off;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        btn_Worker.onClick.AddListener(ClickBtn_Worker);
        btn_Machine.onClick.AddListener(ClickBtn_Machine);
    }
    private void OnEnable()
    {
        ClickBtn_Machine();
    }
    private void ClickBtn_Worker()
    {
        OpenBtn(img_BG_Worker, img_BG_Machine, txt_Worker, txt_Machine);
        OpenUpdateWorkers();
    }
    private void ClickBtn_Machine()
    {
        OpenBtn(img_BG_Machine, img_BG_Worker, txt_Machine, txt_Worker);
        OpenUpdateMachine();
    }
    private void OpenBtn(Image imageOP, Image imageCl, Text textOP, Text textCl)
    {
        imageOP.sprite = spr_BG_On;
        imageCl.sprite = spr_BG_Off;

        textOP.color = color_On;
        textCl.color = color_Off;
    }
    private void OpenUpdateMachine()
    {
        CloseAllUI();
        UI_LabelShow uI_LabelShow = null;
        #region Load UI Level chua max
        foreach (MachineBase machineBase in MachineManager.Instance.allActiveMachine)
        {
            uI_LabelShow = null;
            if (!(machineBase.dataStatusObject as MachineDataStatusObject).isMaxLevelSpeed()
                || !(machineBase.dataStatusObject as MachineDataStatusObject).isMaxLevelStack())
            {
                foreach(UI_LabelShow uI_LabelShow1 in uI_LabelShows)
                {
                    if (uI_LabelShow1.IsClosed())
                    {
                        uI_LabelShow = uI_LabelShow1;
                        uI_LabelShow.Open();
                        break;
                    }
                }
                if(uI_LabelShow == null)
                {
                    uI_LabelShow = Instantiate(uI_LabelShow_Prefab);
                    uI_LabelShow.myTransform.SetParent(transformParent);
                    uI_LabelShows.Add(uI_LabelShow);
                }
              //  Debug.Log((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed() == null);
                uI_LabelShow.myTransform.localScale = Vector3.one;
                uI_LabelShow.LoadUI((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed(), machineBase.dataStatusObject);
                uI_LabelShow.LoadUI((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Stack(), machineBase.dataStatusObject);
            }
        }
        #endregion
      
        #region Load UI level max
        foreach (MachineBase machineBase in MachineManager.Instance.allActiveMachine)
        {
            uI_LabelShow = null;
            if ((machineBase.dataStatusObject as MachineDataStatusObject).isMaxLevelSpeed()
                && (machineBase.dataStatusObject as MachineDataStatusObject).isMaxLevelStack())
            {
                foreach (UI_LabelShow uI_LabelShow1 in uI_LabelShows)
                {
                    if (uI_LabelShow1.IsClosed())
                    {
                        uI_LabelShow = uI_LabelShow1;
                        uI_LabelShow.Open();
                        break;
                    }
                }
                if (uI_LabelShow == null)
                {
                    uI_LabelShow = Instantiate(uI_LabelShow_Prefab);
                    uI_LabelShow.myTransform.SetParent(transformParent);
                    uI_LabelShows.Add(uI_LabelShow);
                }
                uI_LabelShow.myTransform.localScale = Vector3.one;
                uI_LabelShow.LoadUI((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed(), machineBase.dataStatusObject);
                uI_LabelShow.LoadUI((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Stack(), machineBase.dataStatusObject);
            }
        }
        #endregion
    }

    private void OpenUpdateWorkers()
    {
        CloseAllUI();
        UI_LabelShow uI_LabelShow = null;
        #region Load Data Player
        foreach (UI_LabelShow uI_LabelShow1 in uI_LabelShows)
        {
            if (uI_LabelShow1.IsClosed())
            {
                uI_LabelShow = uI_LabelShow1;
                uI_LabelShow.Open();
                break;
            }
        }
        if (uI_LabelShow == null)
        {
            uI_LabelShow = Instantiate(uI_LabelShow_Prefab);
            uI_LabelShow.myTransform.SetParent(transformParent);
            uI_LabelShows.Add(uI_LabelShow);
        }
        //  Debug.Log((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed() == null);
        uI_LabelShow.myTransform.localScale = Vector3.one;
        //uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoSpeedTaget(), "Player"
        //     , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Speed());
        uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoCapacityTaget(), "Player"
             , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Capacity());
        //uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoPriceTaget(), "Player"
        //     , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Price());
        #endregion
        #region Load Data Staff
        foreach(BaseStaff baseStaff in StaffManager.Instance.listAllActiveStaffs)
        {
            uI_LabelShow = null;
            foreach (UI_LabelShow uI_LabelShow1 in uI_LabelShows)
            {
                if (uI_LabelShow1.IsClosed())
                {
                    uI_LabelShow = uI_LabelShow1;
                    uI_LabelShow.Open();
                    break;
                }
            }
            if (uI_LabelShow == null)
            {
                uI_LabelShow = Instantiate(uI_LabelShow_Prefab);
                uI_LabelShow.myTransform.SetParent(transformParent);
                uI_LabelShows.Add(uI_LabelShow);
            }
            //  Debug.Log((machineBase.dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed() == null);
            uI_LabelShow.myTransform.localScale = Vector3.one;
            uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetInfoSpeedTaget(baseStaff.staffType), baseStaff.staffType.ToString().ToLower()
               , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetLevel_Speed(), baseStaff.staffType);
            uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetInfoCapacityTaget(baseStaff.staffType), baseStaff.staffType.ToString().ToLower()
                , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetLevel_Capacity(), baseStaff.staffType);
            //uI_LabelShow.LoadUI(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetInfoHireTaget(baseStaff.staffType), baseStaff.staffType.ToString().ToLower()
            //    , DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(baseStaff.staffType).GetLevel_Hire(), baseStaff.staffType);
        }
        #endregion
    }
    private void CloseAllUI()
    {
        foreach(UI_LabelShow uI_LabelShow in uI_LabelShows)
        {
            uI_LabelShow.CloseAll_GroupUI();
            uI_LabelShow.Close();
        }
    }
    public override void Open()
    {
        base.Open();
        Close();
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
}
