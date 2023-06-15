using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPushBagCloset : MonoBehaviour
{
    [SerializeField]
    private BagCloset closet;
    private BagBase curBag;
    private int v;


    private void OnTriggerEnter(Collider other)
    {
        if (closet.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            player.canCatch = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (!player.canCatch || closet.listBags.Count >= closet.maxObj)
                return;
            switch (closet.type)
            {
                case IngredientType.SHEEP:
                    v = player.sheepCloths.Count - 1;
                    break;
                case IngredientType.COW:
                    v = player.cowCloths.Count - 1;
                    break;
                case IngredientType.CHICKEN:
                    v = player.chickenCloths.Count - 1;
                    break;
                case IngredientType.BEAR:
                    v = player.bearCloths.Count - 1;
                    break;
            }
            if (v >= 0)
            {
                switch (closet.type)
                {
                    case IngredientType.SHEEP:
                        curBag = player.sheepBags[v];
                        break;
                    case IngredientType.COW:
                        curBag = player.cowBags[v];
                        break;
                    case IngredientType.CHICKEN:
                        curBag = player.chickenBags[v];
                        break;
                    case IngredientType.BEAR:
                        curBag = player.bearBags[v];
                        break;
                    default:
                        curBag = null;
                        break;
                }
                if (curBag != null)
                {
                    player.canCatch = false;
                    player.RemoveIngredient(curBag);
                    player.objHave--;
                    //curCloth.transform.DOMoveY(curCloth.transform.position.y + 0.5f, 0.125f).OnComplete(() =>
                    //{
                    //    curCloth.transform.DOJump(closet.transform.position, 0.75f, 1, 0.125f).OnComplete(() =>
                    //    {
                    //        //transform.DOLocalMove(Vector3.up, 0.125f).OnComplete(() =>
                    //        //{

                    //        //});
                    //    }).SetEase(Ease.Linear);
                    //});
                    AllPoolContainer.Instance.Release(curBag);
                    closet.SpawnOutfit();
                    player.DelayCatch(player.timeDelayCatch);
                    (player as BaseActor).ShortObj();
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
