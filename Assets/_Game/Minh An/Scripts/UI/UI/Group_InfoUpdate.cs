using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group_InfoUpdate : UI_Child
{
    public Transform myTransform;
    [Header("Show Info")]
    [SerializeField] private Text txt_Info;
    [Header("Button")]
    [SerializeField] private Button btn_Money;
    [SerializeField] private Text txt_Money;
    [SerializeField] private Image imageBG_Money;
    [SerializeField] private Image imageIcon_Money;
    [SerializeField] private Button btn_Ads;
    [SerializeField] private Text txt_Ads;
    [SerializeField] private Image imageBG_Ads;
    [SerializeField] private Image imageIcon_Ads;
    [Header("Sprite")]
    [SerializeField] private Sprite spr_BG_Green;
    [SerializeField] private Sprite spr_BG_Hong;
    [SerializeField] private Sprite spr_BG_Orange;
    [SerializeField] private Sprite spr_BG_Off;

    private TypeCost typeCost;
    private InfoThis.TypeBuff typeBuff;
    private int Value_MoneyCurent = 0;
    private int Level_Current = 1;
    private DataStatusObject dataStatusObject;
    private InfoPirceObject infoPirceObject;
    private ScriptableObject scriptableObject_Staff_Boss;
    private StaffType staffType = StaffType.CHECKOUT;
    private System.Action actionReloadCurrent;
    private void Awake()
    {
        OnInIt();
    }

    public override void OnInIt()
    {
        base.OnInIt();
        if (myTransform == null) { myTransform = this.transform; }
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ReLoadDataUpgrade.ToString(), ReLoadUpgrade);
    }
    private void ReLoadUpgrade()
    {
        actionReloadCurrent?.Invoke();
    }
    public void InItData(DataStatusObject dataStatusObject, InfoPirceObject infoPirceObject)
    {
        switch (infoPirceObject.infoThese[0].typeBuff)
        {
            case InfoThis.TypeBuff.Speed:
                Level_Current = (dataStatusObject as MachineDataStatusObject).GetLevel_Speed();
                break;
            case InfoThis.TypeBuff.Stack:
                Level_Current = (dataStatusObject as MachineDataStatusObject).GetLevel_Stack();
                break;
        }
        Value_MoneyCurent = infoPirceObject.infoBuys[0].value;
        this.typeCost = infoPirceObject.infoBuys[0].typeCost;
        this.typeBuff = infoPirceObject.infoThese[0].typeBuff;
        this.dataStatusObject = dataStatusObject;
        this.infoPirceObject = infoPirceObject;
      
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                Open_Btn_Ads();
                btn_Ads.onClick.RemoveAllListeners();
                btn_Ads.onClick.AddListener(() => { Buying(TypeCost.WatchVideo); });
                imageIcon_Ads.gameObject.SetActive(true);
                imageBG_Ads.sprite = spr_BG_Hong;
                txt_Ads.text = "FREE";
                break;
            case TypeCost.Money:
                Open_Btn_Money();
                Invoke(nameof(Open_Btn_Money), 0.1f);
                Close_Btn_Ads();
                imageIcon_Money.gameObject.SetActive(true);
                if (DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) >= Value_MoneyCurent)
                {
                    btn_Money.onClick.RemoveAllListeners();
                    btn_Money.onClick.AddListener(() => { Buying(TypeCost.Money); });
                    
                    imageBG_Money.sprite = spr_BG_Green;
                }
                else
                {
                    btn_Money.onClick.RemoveAllListeners();
                    imageBG_Money.sprite = spr_BG_Off;
                    //Open_Btn_Ads();
                    //btn_Ads.onClick.RemoveAllListeners();
                    //btn_Ads.onClick.AddListener(() => { Buying(TypeCost.WatchVideo); });
                    //imageIcon_Ads.gameObject.SetActive(true);
                    //imageBG_Ads.sprite = spr_BG_Hong;
                    //txt_Ads.text = "FREE";
                }

                break;
        }

        btn_Money.enabled = true;
        btn_Ads.enabled = true;
        LoadUI(typeBuff.ToString() + " - Lvl." + Level_Current, Value_MoneyCurent);
        CheckOnNextBuy();
        actionReloadCurrent = ReLoadData;
    }
    public void Open_Btn_Money()
    {
        btn_Money.gameObject.SetActive(true);
        btn_Money.gameObject.SetActive(false);
        btn_Money.gameObject.SetActive(true);
    }
    public void Open_Btn_Ads()
    {
        btn_Money.gameObject.SetActive(false);
        btn_Ads.gameObject.SetActive(true);
    }
    public void Close_Btn_Money()
    {
        btn_Money.gameObject.SetActive(false);
    }
    public void Close_Btn_Ads()
    {
        btn_Ads.gameObject.SetActive(false);
    }
    public void InItData(ScriptableObject scriptableObject, int Level, StaffType staffType = StaffType.CHECKOUT)
    {
        string nameInfo = "";
        this.staffType = staffType;
        Level_Current = Level;
        scriptableObject_Staff_Boss = scriptableObject;
        if (scriptableObject_Staff_Boss as infoCapacity)
        {
            Value_MoneyCurent = (scriptableObject_Staff_Boss as infoCapacity).infoBuys[0].value;
            this.typeCost = (scriptableObject_Staff_Boss as infoCapacity).infoBuys[0].typeCost;
            nameInfo = "Capacity";
        }
        if (scriptableObject_Staff_Boss as infoHire)
        {
            Value_MoneyCurent = (scriptableObject_Staff_Boss as infoHire).infoBuys[0].value;
            this.typeCost = (scriptableObject_Staff_Boss as infoHire).infoBuys[0].typeCost;
            nameInfo = "Hire";
        }
        if (scriptableObject_Staff_Boss as infoSpeed)
        {
            Value_MoneyCurent = (scriptableObject_Staff_Boss as infoSpeed).infoBuys[0].value;
            this.typeCost = (scriptableObject_Staff_Boss as infoSpeed).infoBuys[0].typeCost;
            nameInfo = "Speed";
        }
        if (scriptableObject_Staff_Boss as infoPrice)
        {
            Value_MoneyCurent = (scriptableObject_Staff_Boss as infoPrice).infoBuys[0].value;
            this.typeCost = (scriptableObject_Staff_Boss as infoPrice).infoBuys[0].typeCost;
            nameInfo = "Price";
        }
       
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                Close_Btn_Money();
                Open_Btn_Ads();
                btn_Ads.onClick.RemoveAllListeners();
                btn_Ads.onClick.AddListener(() => { Buying_2(TypeCost.WatchVideo); });
                imageIcon_Ads.gameObject.SetActive(true);
                imageBG_Ads.sprite = spr_BG_Hong;
                txt_Ads.text = "FREE";
                break;
            case TypeCost.Money:
                Open_Btn_Money();
                Invoke(nameof(Open_Btn_Money), 0.1f);
                Close_Btn_Ads();
                imageIcon_Money.gameObject.SetActive(true);
                if (DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) >= Value_MoneyCurent)
                {
                    btn_Money.onClick.RemoveAllListeners();
                    btn_Money.onClick.AddListener(() => { Buying_2(TypeCost.Money); });
                  
                    imageBG_Money.sprite = spr_BG_Green;
                }
                else
                {
                    Debug.Log("Thieu Tien!!!");
                    btn_Money.onClick.RemoveAllListeners();
                    imageBG_Money.sprite = spr_BG_Off;
                    //Close_Btn_Money();
                    //Open_Btn_Ads();
                    //btn_Ads.onClick.RemoveAllListeners();
                    //btn_Ads.onClick.AddListener(() => { Buying_2(TypeCost.WatchVideo); });
                    //imageIcon_Ads.gameObject.SetActive(true);
                    //imageBG_Ads.sprite = spr_BG_Hong;
                    //txt_Ads.text = "FREE";
                }

                break;
        }
        btn_Money.enabled = true;
        btn_Ads.enabled = true;
        LoadUI(nameInfo + " - Lvl." + Level_Current, Value_MoneyCurent);
        CheckOnNextBuy(scriptableObject_Staff_Boss);
        actionReloadCurrent = ReLoadData_2;
    }
    private void LoadUI(string str_Info, int value)
    {
        if (value >= 1000)
        {
            float x = value / 1000;
            txt_Money.text = (x + ((value - 1000 * x) / 1000)).ToString() + "K";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (value > 100)
            txt_Money.text = string.Format("{000}", value);
        else
            txt_Money.text = string.Format("{00}", value);
        txt_Info.text = str_Info;
    }
    private void Buying(TypeCost typeCost)
    {
        #region Check Money
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                Debug.Log("Watch Video");
                switch (typeBuff)
                {
                    case InfoThis.TypeBuff.Speed:
                        (dataStatusObject as MachineDataStatusObject).NextLevel_Speed();
                        break;
                    case InfoThis.TypeBuff.Stack:
                        (dataStatusObject as MachineDataStatusObject).NextLevel_Stack();
                        break;
                }
               
                break;
            case TypeCost.Money:
                Debug.Log("Buy Money");
                DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, Value_MoneyCurent);
                switch (typeBuff)
                {
                    case InfoThis.TypeBuff.Speed:
                        (dataStatusObject as MachineDataStatusObject).NextLevel_Speed();
                        break;
                    case InfoThis.TypeBuff.Stack:
                        (dataStatusObject as MachineDataStatusObject).NextLevel_Stack();
                        break;
                }

                Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
                parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
                parameters[1] = new Firebase.Analytics.Parameter("value", Value_MoneyCurent);
                parameters[2] = new Firebase.Analytics.Parameter("source", "Upgrade_" + dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
                SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);
                break;
        }
        #endregion
        // ReLoadData();
        EnventManager.TriggerEvent(EventName.ReLoadDataUpgrade.ToString());
    }
    private void Buying_2(TypeCost typeCost)
    {
        #region Check Money
        Debug.Log(staffType);
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                Debug.Log("Watch Video");
                if (scriptableObject_Staff_Boss as infoCapacity)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Boss();
                            Level_Current++;
                            break;
                    }

                }
                if (scriptableObject_Staff_Boss as infoHire)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Hire_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Hire_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            break;
                    }
                }
                if (scriptableObject_Staff_Boss as infoSpeed)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Boss();
                            Level_Current++;
                            break;
                    }
                }
                if (scriptableObject_Staff_Boss as infoPrice)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                          
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Price_Boss();
                            break;
                    }
                }
                break;
            case TypeCost.Money:
                Debug.Log("Buy Money");
                DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, Value_MoneyCurent);

                Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
                parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
                parameters[1] = new Firebase.Analytics.Parameter("value", Value_MoneyCurent);
                parameters[2] = new Firebase.Analytics.Parameter("source", "Upgrade_" + staffType.ToString());
                SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);


                if (scriptableObject_Staff_Boss as infoCapacity)
                {

                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Capacity_Boss();
                            Level_Current++;
                            break;
                    }

                }
                if (scriptableObject_Staff_Boss as infoHire)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Hire_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Hire_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            break;
                    }
                }
                if (scriptableObject_Staff_Boss as infoSpeed)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Staff(staffType);
                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Staff(staffType);
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Speed_Boss();
                            Level_Current++;
                            break;
                    }
                }
                if (scriptableObject_Staff_Boss as infoPrice)
                {
                    switch (staffType)
                    {
                        case StaffType.FARMER:

                            Level_Current++;
                            break;
                        case StaffType.WORKER:
                            Level_Current++;
                            break;
                        default:
                            DataManager.Instance.GetDataMap().GetDataMap().NextLevel_Price_Boss();
                            break;
                    }
                }

              
                break;
        }
        #endregion
        // ReLoadData_2();
        EnventManager.TriggerEvent(EventName.ReLoadDataUpgrade.ToString());
    }
    private void CheckOnNextBuy()
    {
        switch (typeBuff)
        {
            case InfoThis.TypeBuff.Speed:
                if ((dataStatusObject as MachineDataStatusObject).isMaxLevelSpeed())
                {
                    btn_Money.onClick.RemoveAllListeners();
                    imageIcon_Money.gameObject.SetActive(false);
                    imageBG_Money.sprite = spr_BG_Orange;
                    txt_Money.text = "MAX";
                    btn_Ads.onClick.RemoveAllListeners();
                    imageIcon_Ads.gameObject.SetActive(false);
                    imageBG_Ads.sprite = spr_BG_Orange;
                    txt_Ads.text = "MAX";
                    btn_Money.enabled = false;
                    btn_Ads.enabled = false;
              
                }
               
                break;
            case InfoThis.TypeBuff.Stack:
                if ((dataStatusObject as MachineDataStatusObject).isMaxLevelStack())
                {
                    btn_Money.onClick.RemoveAllListeners();
                    imageIcon_Money.gameObject.SetActive(false);
                    imageBG_Money.sprite = spr_BG_Orange;
                    txt_Money.text = "MAX";
                    btn_Ads.onClick.RemoveAllListeners();
                    imageIcon_Ads.gameObject.SetActive(false);
                    imageBG_Ads.sprite = spr_BG_Orange;
                    txt_Ads.text = "MAX";
                    btn_Money.enabled = false;
                    btn_Ads.enabled = false;

                }
             
                break;
        }
    }
    private void CheckOnNextBuy(ScriptableObject scriptableObject)
    {
        bool isMax = false;
       
        if (scriptableObject as infoCapacity)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Capacity(staffType, Level_Current + 1);
                 break;
                case StaffType.WORKER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Capacity(staffType, Level_Current + 1);
                    break;
                default:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().IsMaxLevel_Capacity(Level_Current + 1);
                    break;
            }
        }
        if (scriptableObject as infoHire)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Hire(staffType, Level_Current + 1);
                    break;
                case StaffType.WORKER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Hire(staffType, Level_Current + 1);
                    break;
                default:
                    break;
            }
        }
        if (scriptableObject as infoSpeed)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Speed(staffType, Level_Current + 1);
                    break;
                case StaffType.WORKER:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).IsMaxLevel_Speed(staffType, Level_Current + 1);
                    break;
                default:
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().IsMaxLevel_Speed(Level_Current + 1);
                    break;
            }
        }
        if (scriptableObject as infoPrice)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                   
                    break;
                case StaffType.WORKER:
                  
                    break;
                default:
                   // Debug.Log("a");
                    isMax = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().IsMaxLevel_Price(Level_Current + 1);
                    break;
            }
        }
        if (isMax)
        {
            btn_Ads.onClick.RemoveAllListeners();
            btn_Money.onClick.RemoveAllListeners();
            imageIcon_Ads.gameObject.SetActive(false);
            imageBG_Ads.sprite = spr_BG_Orange;
            txt_Ads.text = "MAX";
            imageIcon_Money.gameObject.SetActive(false);
            imageBG_Money.sprite = spr_BG_Orange;
            txt_Money.text = "MAX";
            btn_Money.enabled = false;
            btn_Ads.enabled = false;

        }
       
    }
    private void ReLoadData()
    {
        switch (typeBuff)
        {
            case InfoThis.TypeBuff.Speed:
                infoPirceObject = (dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed();
                break;
            case InfoThis.TypeBuff.Stack:
                infoPirceObject = (dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Stack();
                break;
        }
        InItData(dataStatusObject, infoPirceObject);
    }
    private void ReLoadData_2()
    {
        if (scriptableObject_Staff_Boss as infoCapacity)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoCapacityTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Capacity();
                    break;
                case StaffType.WORKER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoCapacityTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Capacity();
                    break;
                default:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoCapacityTaget();
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Capacity();
                    break;
            }
        }
        if (scriptableObject_Staff_Boss as infoHire)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoHireTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Hire();
                    break;
                case StaffType.WORKER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoHireTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Hire();
                    break;
                default:
                    break;
            }
        }
        if (scriptableObject_Staff_Boss as infoSpeed)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoSpeedTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Speed();
                    break;
                case StaffType.WORKER:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoSpeedTaget(staffType);
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetLevel_Speed();
                    break;
                default:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoSpeedTaget();
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Speed();
                    break;
            }
        }
        if (scriptableObject_Staff_Boss as infoPrice)
        {
            switch (staffType)
            {
                case StaffType.FARMER:

                    break;
                case StaffType.WORKER:

                    break;
                default:
                    scriptableObject_Staff_Boss = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoPriceTaget();
                    Level_Current = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetLevel_Price();
                    break;
            }
        }
        InItData(scriptableObject_Staff_Boss, Level_Current, staffType);
    }
   
}
