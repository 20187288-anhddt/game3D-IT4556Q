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
    [SerializeField] private Button btn_Ads;

    private TypeCost typeCost;
    private InfoThis.TypeBuff typeBuff;
    private int Value_MoneyCurent = 0;
    private int Level_Current = 1;
    private void Awake()
    {
        OnInIt();
    }

    public override void OnInIt()
    {
        base.OnInIt();
        btn_Money.onClick.AddListener(Buying);
        btn_Ads.onClick.AddListener(Buying);
        if (myTransform == null) { myTransform = this.transform; }
    }
    public void InItData(int level, int value_Money, TypeCost typeCost, InfoThis.TypeBuff typeBuff)
    {
        Level_Current = level;
        Value_MoneyCurent = value_Money;
        this.typeCost = typeCost;
        this.typeBuff = typeBuff;
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                btn_Money.gameObject.SetActive(false);
                btn_Ads.gameObject.SetActive(true);
                break;
            case TypeCost.Money:
                btn_Money.gameObject.SetActive(true);
                btn_Ads.gameObject.SetActive(false);
                break;
        }
        LoadUI(typeBuff.ToString() + "-" + level, value_Money.ToString());
    }
    private void LoadUI(string str_Info, string str_Money)
    {
        txt_Info.text = str_Info;
        txt_Money.text = str_Money.ToString();
    }
    private void Buying()
    {
        #region Check Money
        switch (typeCost)
        {
            case TypeCost.WatchVideo:
                Debug.Log("Watch Video");
                break;
            case TypeCost.Money:
                if (DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) >= Value_MoneyCurent)
                {
                    Debug.Log("Buy Money");
                }
                else
                {
                    Debug.Log("Thieu Tien!!!");
                }
                break;
        }
      
        #endregion
    }
}
