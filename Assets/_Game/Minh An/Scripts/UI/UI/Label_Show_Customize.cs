using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label_Show_Customize : UI_Child
{
    public Transform myTransform;
    [SerializeField] private Image img_Icon;
    [Header("Money")]
    [SerializeField] private GameObject obj_Money;
    [SerializeField] private Text txt_Money;
    [Header("Video")]
    [SerializeField] private GameObject obj_Video;
    [SerializeField] private Text txt_Video;

    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void Load(InfoSkinPlayer infoSkinPlayer, bool isBought)
    {
        img_Icon.sprite = infoSkinPlayer.Icon;
        if (!isBought)
        {
            switch (infoSkinPlayer.infoBuy.typeCost)
            {
                case TypeCost.WatchVideo:
                    Open_Obj_Video();
                    //txt_Video.text = infoSkinPlayer.infoBuy.value.ToString();
                    break;
                case TypeCost.Money:
                    Open_Obj_Money();
                    int value = infoSkinPlayer.infoBuy.value;
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
                    break;
            }
        }
        else
        {
            CloseAll();
        }
    }
    private  void Open_Obj_Money()
    {
        obj_Money.SetActive(true);
        obj_Video.SetActive(false);
    }
    private void Open_Obj_Video()
    {
        obj_Money.SetActive(false);
        obj_Video.SetActive(true);
    }
    private void CloseAll()
    {
        obj_Money.SetActive(false);
        obj_Video.SetActive(false);
    }
}
