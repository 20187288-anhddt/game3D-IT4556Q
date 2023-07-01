using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GroupInfoBuffController : Singleton<UI_GroupInfoBuffController>
{
    [SerializeField] private UI_InfoBuff uI_InfoBuff_Prefab;
    [SerializeField] private Transform parent_InfoBuff;
    public void SpawnInfoBuff(NameBonusSpawn nameBonusSpawn, float timeBuff)
    {
        UI_InfoBuff uI_InfoBuff = Instantiate(uI_InfoBuff_Prefab);
        uI_InfoBuff.myTransform.SetParent(parent_InfoBuff);
        uI_InfoBuff.InItInfo(timeBuff);
        uI_InfoBuff.myTransform.localScale = Vector3.one;

        switch (nameBonusSpawn)
        {
            case NameBonusSpawn.Machine_Speed:
                uI_InfoBuff.Icon_Machine_Speed.gameObject.SetActive(true);
                uI_InfoBuff.Icon_Money_Double.gameObject.SetActive(false);
                uI_InfoBuff.Icon_Player_DoubleSpeed.gameObject.SetActive(false);
                break;
            case NameBonusSpawn.Money_Double:
                uI_InfoBuff.Icon_Machine_Speed.gameObject.SetActive(false);
                uI_InfoBuff.Icon_Money_Double.gameObject.SetActive(true);
                uI_InfoBuff.Icon_Player_DoubleSpeed.gameObject.SetActive(false);
                break;
            case NameBonusSpawn.Player_Speed:
                uI_InfoBuff.Icon_Machine_Speed.gameObject.SetActive(false);
                uI_InfoBuff.Icon_Money_Double.gameObject.SetActive(false);
                uI_InfoBuff.Icon_Player_DoubleSpeed.gameObject.SetActive(true);
                break;
        }

    }
    public enum NameBonusSpawn
    {
        Machine_Speed,
        Money_Double,
        Player_Speed,
    }
}
