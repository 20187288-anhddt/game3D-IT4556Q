using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPushCarMission : MonoBehaviour
{
    private IngredientBase curIngredient;
    [SerializeField]
    private CarMission carMission;
    private bool isInItDataUI = false;

    public void OnTriggerEnter(Collider other)
    {
        //if (carMission.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
        //    return;
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (player is Player)
                player.canCatch = true;
            if (!isInItDataUI)
            {
                isInItDataUI = true;
                carMission.InItDataMissionCurrent();
            }
            UI_Manager.Instance.OpenUI(NameUI.Canvas_Order);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            UI_Manager.Instance.CloseUI(NameUI.Canvas_Order);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            int v = -1;
            for(int i = 0;i<carMission.listMission.Keys.Count;i++)
            {
                switch (carMission.listMission.ElementAt(i).Key)
                {
                    case IngredientType.COW_CLOTH:
                        if (carMission.listMission[IngredientType.COW_CLOTH] > 0)
                            v = player.cowCloths.Count - 1;
                        break;
                    case IngredientType.CHICKEN_CLOTH:
                        if (carMission.listMission[IngredientType.CHICKEN_CLOTH] > 0)
                            v = player.chickenCloths.Count - 1;
                        break;
                    case IngredientType.BEAR_CLOTH:
                        if (carMission.listMission[IngredientType.BEAR_CLOTH] > 0)
                            v = player.bearCloths.Count - 1;
                        break;
                    case IngredientType.COW_BAG:
                        if (carMission.listMission[IngredientType.COW_BAG] > 0)
                            v = player.cowBags.Count - 1;
                        break;
                    case IngredientType.CHICKEN_BAG:
                        if (carMission.listMission[IngredientType.CHICKEN_BAG] > 0)
                            v = player.chickenBags.Count - 1;
                        break;
                    case IngredientType.BEAR_BAG:
                        if (carMission.listMission[IngredientType.BEAR_BAG] > 0)
                            v = player.bearBags.Count - 1;
                        break;
                    case IngredientType.LION_CLOTH:
                        if (carMission.listMission[IngredientType.LION_CLOTH] > 0)
                            v = player.lionCloths.Count - 1;
                        break;
                    case IngredientType.CROC_CLOTH:
                        if (carMission.listMission[IngredientType.CROC_CLOTH] > 0)
                            v = player.crocCloths.Count - 1;
                        break;
                    case IngredientType.ELE_CLOTH:
                        if (carMission.listMission[IngredientType.ELE_CLOTH] > 0)
                            v = player.eleCloths.Count - 1;
                        break;
                    case IngredientType.LION_BAG:
                        if (carMission.listMission[IngredientType.LION_BAG] > 0)
                            v = player.lionBags.Count - 1;
                        break;
                    case IngredientType.CROC_BAG:
                        if (carMission.listMission[IngredientType.CROC_BAG] > 0)
                            v = player.crocBags.Count - 1;
                        break;
                    case IngredientType.ELE_BAG:
                        if (carMission.listMission[IngredientType.ELE_BAG] > 0)
                            v = player.eleBags.Count - 1;
                        break;
                    case IngredientType.ZEBRA_CLOTH:
                        if (carMission.listMission[IngredientType.ZEBRA_CLOTH] > 0)
                            v = player.zebraCloths.Count - 1;
                        break;
                    case IngredientType.ZEBRA_BAG:
                        if (carMission.listMission[IngredientType.ZEBRA_BAG] > 0)
                            v = player.zebraBags.Count - 1;
                        break;
                }
                if (v >= 0)
                {
                    if (!player.canCatch || carMission.listMission.Count <= 0)
                        return;
                    player.canCatch = false;
                    switch (carMission.listMission.ElementAt(i).Key)
                    {

                        case IngredientType.COW_CLOTH:
                            curIngredient = player.cowCloths[v];
                            break;
                        case IngredientType.CHICKEN_CLOTH:
                            curIngredient = player.chickenCloths[v];
                            break;
                        case IngredientType.BEAR_CLOTH:
                            curIngredient = player.bearCloths[v];
                            break;
                        case IngredientType.COW_BAG:
                            curIngredient = player.cowBags[v];
                            break;
                        case IngredientType.CHICKEN_BAG:
                            curIngredient = player.chickenBags[v];
                            break;
                        case IngredientType.BEAR_BAG:
                            curIngredient = player.bearBags[v];
                            break;
                        case IngredientType.LION_CLOTH:
                            curIngredient = player.lionCloths[v];
                            break;
                        case IngredientType.CROC_CLOTH:
                            curIngredient = player.crocCloths[v];
                            break;
                        case IngredientType.ELE_CLOTH:
                            curIngredient = player.eleCloths[v];
                            break;
                        case IngredientType.LION_BAG:
                            curIngredient = player.lionBags[v];
                            break;
                        case IngredientType.CROC_BAG:
                            curIngredient = player.crocBags[v];
                            break;
                        case IngredientType.ELE_BAG:
                            curIngredient = player.eleBags[v];
                            break;
                        case IngredientType.ZEBRA_CLOTH:
                            curIngredient = player.zebraBags[v];
                            break;
                        case IngredientType.ZEBRA_BAG:
                            curIngredient = player.zebraBags[v];
                            break;
                        default:
                            curIngredient = null;
                            break;
                    }
                    if (curIngredient != null)
                    {
                        Debug.Log("a");
                        (curIngredient as IngredientBase).MoveToCar(carMission.car);
                        carMission.ReduceType(carMission.listMission.ElementAt(i).Key);
                        player.RemoveIngredient(curIngredient);
                        player.objHave--;
                        //(player as BaseActor).ShortObj();
                        player.DelayCatch(player.timeDelayCatch);
                        //foreach (IngredientType key in carMission.listMission.Keys)
                        //{
                        //    int value = carMission.listMission[key];
                        //    Debug.Log((key, value));
                        //}
                    }
                }
            }
            carMission.UpdateMission();
        }
    }
  
    public void SetisInItDataUI(bool isValue)
    {
        isInItDataUI = isValue;
    }
    public bool GetisInItDataUI()
    {
        return isInItDataUI;
    }
}
