using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Iap : UI_Canvas
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_BuySuperPack;
    [SerializeField] private Button btn_BuyMoneyOffer;
    [SerializeField] private Button btn_BuySimpleOffer;
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        btn_BuySuperPack.onClick.AddListener(() => { BuySuperPack(); });
        btn_BuyMoneyOffer.onClick.AddListener(() => { BuyMoneyOffer(); });
        btn_BuySimpleOffer.onClick.AddListener(() => { BuySimpleOffer(); });
    }
    public override void Open()
    {
        base.Open();
        UI_Manager.Instance.AddUI_To_Stack_UI_Open(this);
    }
    public override void Close()
    {
        base.Close();
        UI_Manager.Instance.ReMoveUI_To_Stack_UI_Open();
    }
    public void BuySuperPack()
    {
        (EventBounsController.Instance?.GetUIBonus(TypeBonus.DoubleSpeed_Player) as UI_Bonus_DoubleSpeed_Player).Reward(1800);
        DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
             DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + 20000);

    }
    public void BuyMoneyOffer()
    {
        DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
            DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + 10000);
    }
    public void BuySimpleOffer()
    {

    }
}
