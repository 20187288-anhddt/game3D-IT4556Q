using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Customize : UI_Canvas
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Label_Show_Customize label_Show_Customize_Prefab;
    [SerializeField] private Transform transFormParent_LabelShow;
    [SerializeField] private List<Label_Show_Customize> label_Show_Customizes = new List<Label_Show_Customize>();

    private void Awake()
    {
        OnInIt();
    }
    private void OnEnable()
    {
        CloseAllUI();
        LoadCustomize();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        LoadCustomize();
    }
    public void LoadCustomize()
    {
        List<int> ints_ID_Open = DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetID_Onboughts();
        Label_Show_Customize show_Customize = null;

        for (int i = 0; i < DataManager.Instance.GetDataCustomizeController().GetInfoSkinPlayers().Count; i++)
        {
            if (ints_ID_Open.Contains(i))
            {
                foreach(Label_Show_Customize label_Show_Customize in label_Show_Customizes)
                {
                    show_Customize = null;
                    if (label_Show_Customize.IsClosed())
                    {
                        label_Show_Customize.Open();
                        show_Customize = label_Show_Customize;
                        break;
                    }
                }
                if(show_Customize == null)
                {
                    show_Customize = Instantiate(label_Show_Customize_Prefab);
                    show_Customize.Open();
                    show_Customize.myTransform.SetParent(transFormParent_LabelShow);
                    label_Show_Customizes.Add(show_Customize);
                }
                show_Customize.Load(GetInfoSkinPlayer(i), true, GetInfoSkinPlayer(i).ID);
            }
        }
        for (int i = 0; i < DataManager.Instance.GetDataCustomizeController().GetInfoSkinPlayers().Count; i++)
        {
            if (!ints_ID_Open.Contains(i))
            {
                foreach (Label_Show_Customize label_Show_Customize in label_Show_Customizes)
                {
                    show_Customize = null;
                    if (label_Show_Customize.IsClosed())
                    {
                        label_Show_Customize.Open();
                        show_Customize = label_Show_Customize;
                        break;
                    }
                }
                if (show_Customize == null)
                {
                    show_Customize = Instantiate(label_Show_Customize_Prefab);
                    show_Customize.Open();
                    show_Customize.myTransform.SetParent(transFormParent_LabelShow);
                    label_Show_Customizes.Add(show_Customize);
                }
                show_Customize.Load(GetInfoSkinPlayer(i), false, GetInfoSkinPlayer(i).ID);
            }
        }
    }
    public InfoSkinPlayer GetInfoSkinPlayer(int ID)
    {
        InfoSkinPlayer infoSkinPlayer = DataManager.Instance.GetDataCustomizeController().GetInfoSkinPlayers()[ID];
        return infoSkinPlayer;
    }
    private void CloseAllUI()
    {
        foreach (Label_Show_Customize label_Show_Customize in label_Show_Customizes)
        {
            label_Show_Customize.Close();
        }
    }
    public override void Open()
    {
        base.Open();
        Close();
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
}
