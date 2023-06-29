using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckPushCloset : MonoBehaviour
{
    [SerializeField]
    private Closet closet;
    private ClothBase curCloth;
    private int v;


    private void OnTriggerEnter(Collider other)
    {
        if (closet.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            if (player is Player)
                player.canCatch = true;
            if (player is Staff)
            {
                if ((player as Staff).ingredientType == closet.ingredientType)
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
                if ((player as Staff).ingredientType != closet.ingredientType)
                {
                    return;
                };
            }
            if (!player.canCatch || closet.listOutfits.Count >= closet.maxObj)
                return;         
            switch (closet.ingredientType)
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
                switch (closet.ingredientType)
                {
                    case IngredientType.SHEEP:
                        curCloth = player.sheepCloths[v];
                        break;
                    case IngredientType.COW:
                        curCloth = player.cowCloths[v];
                        break;
                    case IngredientType.CHICKEN:
                        curCloth = player.chickenCloths[v];
                        break;
                    case IngredientType.BEAR:
                        curCloth = player.bearCloths[v];
                        break;
                    default:
                        curCloth = null;
                        break;
                }
                if (curCloth != null)
                {
                    player.canCatch = false;
                    player.RemoveIngredient(curCloth);  
                    player.objHave--;
                    //curCloth.transform.DOJump(closet.transform.position, 3f, 1, 0.5f).OnComplete(() =>
                    //{
                    //    closet.SpawnOutfit();
                    //    AllPoolContainer.Instance.Release(curCloth);
                    //}).SetEase(Ease.OutCirc);
                    AllPoolContainer.Instance.Release(curCloth);
                    closet.SpawnOutfit();
                    player.DelayCatch(player.timeDelayCatch);
                    //(player as BaseActor).ShortObj();     
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
