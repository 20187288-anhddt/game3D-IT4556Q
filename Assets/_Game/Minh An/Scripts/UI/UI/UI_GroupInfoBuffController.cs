using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GroupInfoBuffController : Singleton<UI_GroupInfoBuffController>
{
    [SerializeField] private UI_InfoBuff uI_InfoBuff_Prefab;
    [SerializeField] private Transform parent_InfoBuff;
    [SerializeField] private Dictionary<NameBonusSpawn, UI_InfoBuff> uI_InfoBuffs = new Dictionary<NameBonusSpawn, UI_InfoBuff>();
    public void SpawnInfoBuff(NameBonusSpawn nameBonusSpawn, float timeBuff, System.Action actionStopBuff)
    {
        UI_InfoBuff uI_InfoBuff = null;
        if (uI_InfoBuffs.ContainsKey(nameBonusSpawn))
        {
            uI_InfoBuffs.TryGetValue(nameBonusSpawn, out uI_InfoBuff);
        }
        if(uI_InfoBuff == null)
        {
            uI_InfoBuff = Instantiate(uI_InfoBuff_Prefab);
            uI_InfoBuff.myTransform.SetParent(parent_InfoBuff);
            if (uI_InfoBuffs.ContainsKey(nameBonusSpawn))
            {
                uI_InfoBuffs[nameBonusSpawn] = uI_InfoBuff;
            }
            else
            {
                uI_InfoBuffs.Add(nameBonusSpawn, uI_InfoBuff);
            }
        }
        uI_InfoBuff.SetActionStopBuff(actionStopBuff);
        uI_InfoBuff.InItInfo(timeBuff);
        uI_InfoBuff.myTransform.localScale = Vector3.one;
        
        switch (nameBonusSpawn)
        {
            case NameBonusSpawn.Machine_Speed:
                uI_InfoBuff.Icon_Machine_Speed.SetActive(true);
                uI_InfoBuff.Icon_Money_Double.SetActive(false);
                uI_InfoBuff.Icon_Player_DoubleSpeed.SetActive(false);
                uI_InfoBuff.Icon_NoShit.SetActive(false);
                break;
            case NameBonusSpawn.Money_Double:
                uI_InfoBuff.Icon_Machine_Speed.SetActive(false);
                uI_InfoBuff.Icon_Money_Double.SetActive(true);
                uI_InfoBuff.Icon_Player_DoubleSpeed.SetActive(false);
                uI_InfoBuff.Icon_NoShit.SetActive(false);
                break;
            case NameBonusSpawn.Player_Speed:
                uI_InfoBuff.Icon_Machine_Speed.SetActive(false);
                uI_InfoBuff.Icon_Money_Double.SetActive(false);
                uI_InfoBuff.Icon_Player_DoubleSpeed.SetActive(true);
                uI_InfoBuff.Icon_NoShit.SetActive(false);
                break;
            case NameBonusSpawn.NoShit:
                uI_InfoBuff.Icon_Machine_Speed.SetActive(false);
                uI_InfoBuff.Icon_Money_Double.SetActive(false);
                uI_InfoBuff.Icon_Player_DoubleSpeed.SetActive(false);
                uI_InfoBuff.Icon_NoShit.SetActive(true);
                break;
        }

    }
    public enum NameBonusSpawn
    {
        Machine_Speed,
        Money_Double,
        Player_Speed,
        NoShit,
    }
}
