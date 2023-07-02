using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Process_Item : UI_Child
{
    public Transform myTransform;
    [SerializeField] private Text txt_Process;
    [SerializeField] private RectTransform rectTransform_BG_Process;
    [SerializeField] private RectTransform rect_Process_Loading;
    [Header("Icon")]
    [SerializeField] private Image img_Icon;
    [SerializeField] private Sprite spr_ChickenCloth;
    [SerializeField] private Sprite spr_ChickenBag;
    [SerializeField] private Sprite spr_CowCloth;
    [SerializeField] private Sprite spr_CowBag;
    [SerializeField] private Sprite spr_BearCloth;
    [SerializeField] private Sprite spr_BearBag;

    private IngredientType ingredientTypeCurrent;
    private int valueMax = 0;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void LoadProcess(int valueCurrent)
    {
        valueCurrent = valueMax - valueCurrent;
        txt_Process.text = valueCurrent.ToString() + "/" + valueMax.ToString();
        rect_Process_Loading.sizeDelta = Vector2.right * rectTransform_BG_Process.rect.width * valueCurrent * 1.0f / valueMax + Vector2.up * rect_Process_Loading.rect.height;
        myTransform.localScale = Vector3.one;
    }
    public void InItData(int valueMax, IngredientType ingredientTypeCurrent)
    {
       // Debug.Log(valueMax);
        this.valueMax = valueMax;
        this.ingredientTypeCurrent = ingredientTypeCurrent;
        myTransform.transform.localScale = Vector3.one;
        LoadSpriteIcon();
    }
    private void LoadSpriteIcon()
    {
        switch (ingredientTypeCurrent)
        {
            case IngredientType.CHICKEN_CLOTH:
                img_Icon.sprite = spr_ChickenCloth;
                break;
            case IngredientType.COW_CLOTH:
                img_Icon.sprite = spr_CowCloth;
                break;
            case IngredientType.BEAR_CLOTH:
                img_Icon.sprite = spr_BearCloth;
                break;
            case IngredientType.CHICKEN_BAG:
                img_Icon.sprite = spr_ChickenBag;
                break;
            case IngredientType.COW_BAG:
                img_Icon.sprite = spr_CowBag;
                break;
            case IngredientType.BEAR_BAG:
                img_Icon.sprite = spr_BearBag;
                break;
        }
    }
    public IngredientType GetIngredientType()
    {
        return ingredientTypeCurrent;
    }
    public int GetValueMax()
    {
       // Debug.Log(valueMax);
        return valueMax;
    }
}
