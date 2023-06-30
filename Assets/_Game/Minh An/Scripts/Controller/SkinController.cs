using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] private List<Transform> transformSet_Heads;
    private Transform trans_Hat_Current;
    private void Start()
    {
        OnInIt();
    }
    private void OnInIt()
    {
        LoadHead();
        EnventManager.AddListener(EventName.NewID_Customize.ToString(), LoadHead);
    }
    public void LoadHead()
    {
        if(trans_Hat_Current == null)
        {
            trans_Hat_Current = Instantiate((GameObject)DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetInfoSkinPlayer_Current().modelSkin).transform;
            trans_Hat_Current.SetParent(transformSet_Heads[DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetID()-1]);
            trans_Hat_Current.localPosition = Vector3.zero;
            trans_Hat_Current.localRotation = Quaternion.identity;   
        }
        else
        {
            Destroy(trans_Hat_Current.gameObject);
            trans_Hat_Current = null;
            LoadHead();
        }
    }
}
