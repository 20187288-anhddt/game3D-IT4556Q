using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPush : MonoBehaviour
{
    [SerializeField]
    private MachineBase machine;
    private IngredientBase curIngredient;
    private int v;

    private void OnTriggerEnter(Collider other)
    {
        if (machine.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (player is Player)
                player.canCatch = true;
            if (player is Staff)
            {
                if ((player as Staff).ingredientType == machine.ingredientType)
                {
                    player.canCatch = true;
                };
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (player is Staff)
            {
                if ((player as Staff).ingredientType != machine.ingredientType)
                {
                    return;
                };
            }
            switch (machine.ingredientType)
            {
                case IngredientType.SHEEP:
                    v = player.fleeces.Count - 1;
                    break;
                case IngredientType.COW:
                    v = player.cowFurs.Count - 1;
                    break;
                case IngredientType.CHICKEN:
                    v = player.chickenFurs.Count - 1;
                    break;
                case IngredientType.BEAR:
                    v = player.bearFurs.Count - 1;
                    break;
            }
            
            if (v >= 0)
            {
                if (!player.canCatch || machine.ingredients.Count >= machine.maxObjInput)
                    return;
                player.canCatch = false;
                switch (machine.ingredientType)
                {
                    case IngredientType.SHEEP:
                        curIngredient = player.fleeces[v];
                        break;
                    case IngredientType.COW:
                        curIngredient = player.cowFurs[v];
                        break;
                    case IngredientType.CHICKEN:
                        curIngredient = player.chickenFurs[v];
                        break;
                    case IngredientType.BEAR:
                        curIngredient = player.bearFurs[v];
                        break;
                    default:
                        curIngredient = null;
                        break;
                }
                if(curIngredient != null)
                {
                    (curIngredient as FurBase).MoveToMachine(machine);
                    player.RemoveIngredient(curIngredient);
                    player.objHave--;
                    //(player as BaseActor).ShortObj();
                    player.DelayCatch(player.timeDelayCatch);
                }    
            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    var player = other.GetComponent<ICollect>();
    //    if (player != null)
    //    {
    //        if (player.isTiming)
    //        {
    //            player.CancelDelay(player.timeDelayCatch);
    //        }
    //        player.canCatch = false;
    //    }
    //}
}
