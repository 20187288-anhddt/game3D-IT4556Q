using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPushCarMission : MonoBehaviour
{
    private IngredientBase curIngredient;
    [SerializeField]
    private CarMission carMission;
    private void OnTriggerEnter(Collider other)
    {
        //if (carMission.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
        //    return;
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (player is Player)
                player.canCatch = true;
        }
    }
    private void OnTriggerStay(Collider other)
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
                        default:
                            curIngredient = null;
                            break;
                    }
                    if (curIngredient != null)
                    {
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
        }
    }
}
