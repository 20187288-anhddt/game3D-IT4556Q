using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Process_Item : UI_Child
{
    public Transform myTransform;
    [SerializeField] private Image img_Icon;
    [SerializeField] private Text txt_Process;
    [SerializeField] private RectTransform rectTransform_BG_Process;
    [SerializeField] private RectTransform rect_Process_Loading;
    private IngredientType ingredientTypeCurrent;
    private int valueMax = 0;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void LoadProcess(int valueCurrent, Sprite Icon)
    {
        valueCurrent = valueMax - valueCurrent;
        img_Icon.sprite = Icon;
        txt_Process.text = valueCurrent.ToString() + "/" + valueMax.ToString();
        rect_Process_Loading.sizeDelta = Vector2.right * rectTransform_BG_Process.rect.width * valueCurrent * 1.0f / valueMax + Vector2.up * rect_Process_Loading.rect.height;
        myTransform.localScale = Vector3.one;
    }
    public void InItData(int valueMax, IngredientType ingredientTypeCurrent)
    {
        this.valueMax = valueMax;
        this.ingredientTypeCurrent = ingredientTypeCurrent;
        myTransform.transform.localScale = Vector3.one;
    }
    public IngredientType GetIngredientType()
    {
        return ingredientTypeCurrent;
    }
    public int GetValueMax()
    {
        return valueMax;
    }
}
