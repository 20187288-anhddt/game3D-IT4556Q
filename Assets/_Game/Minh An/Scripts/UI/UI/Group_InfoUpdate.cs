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
    [SerializeField] private Sprite spr_BG_Off;

    private TypeCost typeCost;
    private InfoThis.TypeBuff typeBuff;
    private int Value_MoneyCurent = 0;
    private int Level_Current = 1;
    private DataStatusObject dataStatusObject;
    private InfoPirceObject infoPirceObject;
    private void Awake()
    {
        OnInIt();
    }

    public override void OnInIt()
    {
        base.OnInIt();
        if (myTransform == null) { myTransform = this.transform; }
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
                btn_Money.gameObject.SetActive(false);
                btn_Ads.gameObject.SetActive(true);
                btn_Ads.onClick.RemoveAllListeners();
                btn_Ads.onClick.AddListener(Buying);
                imageIcon_Ads.gameObject.SetActive(true);
                imageBG_Ads.sprite = spr_BG_Hong;
                txt_Ads.text = "FREE";
                break;
            case TypeCost.Money:
                btn_Money.gameObject.SetActive(true);
                btn_Ads.gameObject.SetActive(false);
                if (DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) >= Value_MoneyCurent)
                {
                    btn_Money.onClick.RemoveAllListeners();
                    btn_Money.onClick.AddListener(Buying);
                    imageIcon_Money.gameObject.SetActive(true);
                    imageBG_Money.sprite = spr_BG_Green;
                }
                else
                {
                    Debug.Log("Thieu Tien!!!");
                    btn_Money.onClick.RemoveAllListeners();
                    imageBG_Money.sprite = spr_BG_Off;
                }

                break;
        }

        LoadUI(typeBuff.ToString() + " - Lvl." + Level_Current, Value_MoneyCurent);
        CheckOnNextBuy();
    }
    private void LoadUI(string str_Info, int value)
    {
        if (value > 1000)
        {
            float x = value / 1000;
            txt_Money.text = (x + ((value - 1000 * x) / 1000)).ToString("F2") + "K";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (value > 100)
            txt_Money.text = string.Format("{000}", value);
        else
            txt_Money.text = string.Format("{00}", value);
        txt_Info.text = str_Info;
    }
    private void Buying()
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
        }
        #endregion
        ReLoadData();
    }
    private void CheckOnNextBuy()
    {
        switch (typeBuff)
        {
            case InfoThis.TypeBuff.Speed:
                if ((dataStatusObject as MachineDataStatusObject).isMaxLevelSpeed())
                {
                    switch (typeCost)
                    {
                        case TypeCost.WatchVideo:
                            btn_Ads.onClick.RemoveAllListeners();
                            imageIcon_Ads.gameObject.SetActive(false);
                            imageBG_Ads.sprite = spr_BG_Green;
                            txt_Ads.text = "MAX";
                            break;
                        case TypeCost.Money:
                            btn_Money.onClick.RemoveAllListeners();
                            imageIcon_Money.gameObject.SetActive(false);
                            imageBG_Money.sprite = spr_BG_Green;
                            txt_Money.text = "MAX";
                            break;
                    }
                  
                }
               
                break;
            case InfoThis.TypeBuff.Stack:
                if ((dataStatusObject as MachineDataStatusObject).isMaxLevelStack())
                {
                    switch (typeCost)
                    {
                        case TypeCost.WatchVideo:
                            btn_Ads.onClick.RemoveAllListeners();
                            imageIcon_Ads.gameObject.SetActive(false);
                            imageBG_Ads.sprite = spr_BG_Green;
                            txt_Ads.text = "MAX";
                            break;
                        case TypeCost.Money:
                            btn_Money.onClick.RemoveAllListeners();
                            imageIcon_Money.gameObject.SetActive(false);
                            imageBG_Money.sprite = spr_BG_Green;
                            txt_Money.text = "MAX";
                            break;
                    }
                }
             
                break;
        }
    }
    private void ReLoadData()
    {
        InItData(dataStatusObject, infoPirceObject);
    }
}
