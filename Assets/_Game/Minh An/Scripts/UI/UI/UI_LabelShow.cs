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
    [SerializeField] private Text txt_Name;
    [SerializeField] private Transform transParent;
    [SerializeField] private Group_InfoUpdate group_InfoUpdate_Prefab;
    [SerializeField] private List<Group_InfoUpdate> group_InfoUpdates;

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
        txt_Name.text = infoPirceObject.nameString;
        group_InfoUpdate_.myTransform.localScale = Vector3.one;
        group_InfoUpdate_.InItData(dataStatusObject, infoPirceObject);
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
        if (count_ < 2)
        {
            myRectTransform.sizeDelta = Vector2.right * myRectTransform.rect.width + Vector2.up * HeightSize_OneLength;
        }
        else
        {
            myRectTransform.sizeDelta = Vector2.right * myRectTransform.rect.width + Vector2.up * HeightSize_TwoLength;
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
