using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_TableLabelBuff_Money : UI_TableLabelBuff
{
    [SerializeField] private Text txt_Money;

    public void LoadMoney(int moneyBuff)
    {
        if (moneyBuff > 1000)
        {
            float x = moneyBuff / 1000;
            txt_Money.text = "+" + (x + ((moneyBuff - 1000 * x) / 1000)).ToString("F2") + "K";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (moneyBuff > 100)
            txt_Money.text = "+" + string.Format("{000}", moneyBuff);
        else
            txt_Money.text = "+" + string.Format("{00}", moneyBuff);
    }
}
