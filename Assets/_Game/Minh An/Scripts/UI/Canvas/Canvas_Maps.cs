using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Maps : UI_Canvas
{
    [System.Serializable]
    private struct Setting
    {
        public Transform transformParent;
        public UI_LabelMaps uI_LabelMaps_Prefab;
    }
    [Header("Button")]
    [SerializeField] private Button btn_Close;
    [SerializeField] private Dictionary<int, UI_LabelMaps> uI_LabelMaps = new Dictionary<int, UI_LabelMaps>();
    [SerializeField] private Setting setting;
    private void Awake()
    {
        AddEvent();
    }
    private void AddEvent()
    {
        btn_Close.onClick.AddListener(Close);
    }
    private void OnEnable()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        LoadUI();
    }
    private void LoadUI()
    {
        int maxIdMap = LoadMapController.Instance.GetCountMaxMap();
        UI_LabelMaps uI_LabelMaps_ = null;
        for (int i = 0; i < maxIdMap; i++)
        {
            if (!uI_LabelMaps.ContainsKey(i))
            {
                uI_LabelMaps_ = Instantiate(setting.uI_LabelMaps_Prefab, setting.transformParent);
                uI_LabelMaps.Add(i, uI_LabelMaps_);
            }
            else
            {
                uI_LabelMaps_ = uI_LabelMaps[i];
            }
            uI_LabelMaps_.LoadLabel(i);
        }
    }
}
