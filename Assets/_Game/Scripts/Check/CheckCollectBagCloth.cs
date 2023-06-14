using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollectBagCloth : MonoBehaviour
{
    [SerializeField]
    private BagMachine machine;

    private void OnTriggerEnter(Collider other)
    {
        if (machine.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        //if (player != null)
        //{
        if (player is Player)
                player.canCatch = true;
            if (player is Staff)
            {
                if ((player as Staff).ingredientType == machine.ingredientType || (player as Staff).curMachine != this.machine)
                {
                    player.canCatch = true;
                };
            }
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (machine.isLock || machine.outCloths.Count <= 0)
            return;
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        if (/*player != null && */player.objHave < player.maxCollectObj)
        {
            if (player is Staff)
            {
                if ((player as Staff).ingredientType != machine.ingredientType || (player as Staff).curMachine != this.machine)
                {
                    return;
                };
            }
            int value = machine.outCloths.Count - 1;
            if (value < 0)
                return;
            if (!player.canCatch)
                return;
            player.canCatch = false;
            var curCloth = machine.outCloths[value];
            curCloth.MoveToICollect(player);
            machine.outCloths.Remove(curCloth);
            machine.numOutputSave--;
            player.AddIngredient(curCloth);
            //(player as BaseActor).ShortObj();
            player.DelayCatch(player.timeDelayCatch);
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
