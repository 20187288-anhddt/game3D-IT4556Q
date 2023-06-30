using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GroupInfoBuffController : Singleton<UI_GroupInfoBuffController>
{
    [SerializeField] private UI_InfoBuff uI_InfoBuff_Prefab;
    [SerializeField] private Transform parent_InfoBuff;
    public void SpawnInfoBuff(float timeBuff)
    {
        UI_InfoBuff uI_InfoBuff = Instantiate(uI_InfoBuff_Prefab);
        uI_InfoBuff.myTransform.SetParent(parent_InfoBuff);
        uI_InfoBuff.InItInfo(timeBuff);
        uI_InfoBuff.myTransform.localScale = Vector3.one;
    }

}
