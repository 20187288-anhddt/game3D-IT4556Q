using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPushCarMission : MonoBehaviour
{
    private int v;
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
            foreach (IngredientType key in carMission.listMission.Keys)
            {
                switch (key)
                {
                    case IngredientType.COW_CLOTH:
                        v = player.cowCloths.Count - 1;
                        break;
                    case IngredientType.CHICKEN_CLOTH:
                        v = player.chickenCloths.Count - 1;
                        break;
                    case IngredientType.BEAR_CLOTH:
                        v = player.bearCloths.Count - 1;
                        break;
                    case IngredientType.COW_BAG:
                        v = player.cowBags.Count - 1;
                        break;
                    case IngredientType.CHICKEN_BAG:
                        v = player.chickenBags.Count - 1;
                        break;
                    case IngredientType.BEAR_BAG:
                        v = player.bearBags.Count - 1;
                        break;
                }
                if (v >= 0)
                {
                    if (!player.canCatch || carMission.listMission.Count <= 0)
                        return;
                    player.canCatch = false;
                    switch (key)
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
                        (curIngredient as IngredientBase).MoveToCar(carMission);
                        carMission.ReduceType(key);
                        player.RemoveIngredient(curIngredient);
                        player.objHave--;
                        //(player as BaseActor).ShortObj();
                        player.DelayCatch(player.timeDelayCatch);
                    }
                }
            }
        }
    }
}
