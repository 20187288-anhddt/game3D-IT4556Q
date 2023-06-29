using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LabelShow : UI_Child
{
    public Transform myTransform;
    public RectTransform myRectTransform;
    public static float HeightSize_OneLength = 663;
    public static float HeightSize_TwoLength = 783;
    public static float HeightSize_ThreeLength = 1120;
    [SerializeField] private Text txt_Name;
    [SerializeField] private Transform transParent;
    [SerializeField] private Group_InfoUpdate group_InfoUpdate_Prefab;
    [SerializeField] private List<Group_InfoUpdate> group_InfoUpdates;
    [Header("Icon")]
    [SerializeField] private Image IconThis;
    [SerializeField] private Sprite spr_Player;
    [SerializeField] private Sprite spr_Staff_Farmer;
    [SerializeField] private Sprite spr_Staff_Worker;
    [SerializeField] private Sprite spr_Machine_Chicken_Cloth;
    [SerializeField] private Sprite spr_Machine_Chicken_Bag;
    [SerializeField] private Sprite spr_Machine_Cow_Cloth;
    [SerializeField] private Sprite spr_Machine_Cow_Bag;
    [SerializeField] private Sprite spr_Machine_Bear_Cloth;
    [SerializeField] private Sprite spr_Machine_Bear_Bag;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        if(myRectTransform == null) { myRectTransform = GetComponent<RectTransform>(); }
        if(myTransform == null) { myTransform = this.transform; }
    }
    private void OnDisable()
    {
        foreach(Group_InfoUpdate group_InfoUpdate in group_InfoUpdates)
        {
            group_InfoUpdate.Close();
        }
    }
    public void LoadUI(InfoPirceObject infoPirceObject, DataStatusObject dataStatusObject)
    {
        Group_InfoUpdate group_InfoUpdate_ = null; 
        foreach (Group_InfoUpdate group_InfoUpdate in group_InfoUpdates)
        {
            if (group_InfoUpdate.IsClosed())
            {
                group_InfoUpdate_ = group_InfoUpdate;
                group_InfoUpdate_.Open();
                break;
            }
        }
        if(group_InfoUpdate_ == null)
        {
            group_InfoUpdate_ = Instantiate(group_InfoUpdate_Prefab);
            group_InfoUpdate_.myTransform.SetParent(transParent);
            group_InfoUpdates.Add(group_InfoUpdate_);
        }
        txt_Name.text = infoPirceObject.nameString/*.Replace(' ', '\n')*/;
        group_InfoUpdate_.myTransform.localScale = Vector3.one;
        group_InfoUpdate_.InItData(dataStatusObject, infoPirceObject);
        switch ((dataStatusObject as MachineDataStatusObject).GetBaseBuild().nameObject_This)
        {
            case NameObject_This.ChickenClothMachine:
                IconThis.sprite = spr_Machine_Chicken_Cloth;
                break;
            case NameObject_This.CowClothMachine:
                IconThis.sprite = spr_Machine_Cow_Cloth;
                break;
            case NameObject_This.BearClothMachine:
                IconThis.sprite = spr_Machine_Bear_Cloth;
                break;
            case NameObject_This.ChickenBagMachine:
                IconThis.sprite = spr_Machine_Chicken_Bag;
                break;
            case NameObject_This.CowBagMachine:
                IconThis.sprite = spr_Machine_Cow_Bag;
                break;
            case NameObject_This.BearBagMachine:
                IconThis.sprite = spr_Machine_Bear_Bag;
                break;
        }
        UpdateSizeLabel();
    }
    public void LoadUI(ScriptableObject scriptableObject, string name, int Level, StaffType staffType = StaffType.CHECKOUT)
    {
        Group_InfoUpdate group_InfoUpdate_ = null;
        foreach (Group_InfoUpdate group_InfoUpdate in group_InfoUpdates)
        {
            if (group_InfoUpdate.IsClosed())
            {
                group_InfoUpdate_ = group_InfoUpdate;
                group_InfoUpdate_.Open();
                break;
            }
        }
        if (group_InfoUpdate_ == null)
        {
            group_InfoUpdate_ = Instantiate(group_InfoUpdate_Prefab);
            group_InfoUpdate_.myTransform.SetParent(transParent);
            group_InfoUpdates.Add(group_InfoUpdate_);
        }
       // name = name.Replace(' ', '\n');
        txt_Name.text = name;
        group_InfoUpdate_.myTransform.localScale = Vector3.one;
        group_InfoUpdate_.InItData(scriptableObject, Level, staffType);
        switch (staffType)
        {
            case StaffType.FARMER:
                IconThis.sprite = spr_Staff_Farmer;
                break;
            case StaffType.WORKER:
                IconThis.sprite = spr_Staff_Worker;
                break;
            default:
                IconThis.sprite = spr_Player;
                break;
        }
        UpdateSizeLabel();
    }
    private void UpdateSizeLabel()
    {
        int count_ = 0;
        foreach (Group_InfoUpdate group_InfoUpdate in group_InfoUpdates)
        {
            if (group_InfoUpdate.IsOpend())
            {
                count_++;
            }
        }
        if (count_ == 1)
        {
            myRectTransform.sizeDelta = Vector2.right * myRectTransform.rect.width + Vector2.up * HeightSize_OneLength;
        }
        else if(count_ == 2)
        {
            myRectTransform.sizeDelta = Vector2.right * myRectTransform.rect.width + Vector2.up * HeightSize_TwoLength;
        }
        else
        {
            myRectTransform.sizeDelta = Vector2.right * myRectTransform.rect.width + Vector2.up * HeightSize_ThreeLength;
        }
    }
    public void CloseAll_GroupUI()
    {
        foreach(Group_InfoUpdate group_InfoUpdate in group_InfoUpdates)
        {
            group_InfoUpdate.Close();
        }
    }
}
