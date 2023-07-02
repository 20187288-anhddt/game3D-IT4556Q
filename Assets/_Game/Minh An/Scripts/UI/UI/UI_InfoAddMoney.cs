using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoAddMoney : UI_Child
{
    [SerializeField] private Text txt_Money;
    [Header("BackGround")]
    [SerializeField] private Image img_BG;
    [SerializeField] private Color colorAdd;
    [SerializeField] private Color colorRemove;
    [Header("Animator")]
    [SerializeField] private RectTransform rectTransformThis;
    [SerializeField] private float Speed;
    private Vector3 posStart;
    private Vector3 posEnd;
    float m_time = 0;
    private bool isPlayAnim;
    private void Start()
    {
       // animatorThis.Play("None");
    }
    public void Show(int MoneyShow, TypeShow typeShow)
    {
        Open();
        string dau = "+";
        switch (typeShow)
        {
            case TypeShow.AddMoney:
                dau = "+";
                img_BG.color = colorAdd;
                break;
            case TypeShow.RemoveMoney:
                dau = "-";
                img_BG.color = colorRemove;
                break;
        }
        
        if (MoneyShow > 1000)
        {
            float x = MoneyShow / 1000;
            txt_Money.text = dau + (x + ((MoneyShow - 1000 * x) / 1000)).ToString("F2") + "K" + " ";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (MoneyShow > 100)
            txt_Money.text = dau + string.Format("{000}", MoneyShow);
        else
        {
            txt_Money.text = dau + string.Format("{00}", MoneyShow);
        }
        posStart = rectTransformThis.anchoredPosition;
        posEnd = Vector3.right * posStart.x + Vector3.up * 65;
        isPlayAnim = true;
        // animatorThis.Play(NameAnimShow);

    }
    private void Update()
    {
        if (isPlayAnim)
        {
            if (m_time < 1)
            {
                rectTransformThis.anchoredPosition = Vector3.Lerp(posStart, posEnd, m_time);
                m_time += Time.deltaTime * Speed;
            }
            else
            {
                if(-posEnd.y == 100)
                {
                    isPlayAnim = false;
                }
                m_time = 0;
                posEnd = Vector3.right * posStart.x + Vector3.up * -100;
            }
        }
      
    }
   
    public enum TypeShow
    {
        AddMoney,
        RemoveMoney
    }

}
