using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
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
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        //if (player != null)
        //{
        if (player is Player)
                player.canCatch = true;
            if (player is Staff)
            {
                if ((player as Staff).ingredientType == closet.ingredientType)
                {
                    player.canCatch = true;
                };
            }
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        //if (player != null)
        //{
        if (player is Staff)
            {
                if ((player as Staff).ingredientType != closet.ingredientType)
                {
                    return;
                };
            }
            if (!player.canCatch || closet.listBags.Count >= closet.maxObj)
                return;
            switch (closet.ingredientType)
            {
                case IngredientType.SHEEP:
                    v = player.sheepBags.Count - 1;
                    break;
                case IngredientType.COW:
                    v = player.cowBags.Count - 1;
                    break;
                case IngredientType.CHICKEN:
                    v = player.chickenBags.Count - 1;
                    break;
                case IngredientType.BEAR:
                    v = player.bearBags.Count - 1;
                    break;
                case IngredientType.LION:
                    v = player.lionBags.Count - 1;
                    break;
                case IngredientType.CROC:
                    v = player.crocBags.Count - 1;
                    break;
                case IngredientType.ELE:
                    v = player.eleBags.Count - 1;
                    break;
                case IngredientType.ZEBRA:
                    v = player.zebraBags.Count - 1;
                    break;
            }
            if (v >= 0)
            {
               // Debug.Log("a");
                switch (closet.ingredientType)
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
                    case IngredientType.LION:
                        curBag = player.lionBags[v];
                        break;
                    case IngredientType.CROC:
                        curBag = player.crocBags[v];
                        break;
                    case IngredientType.ELE:
                        curBag = player.eleBags[v];
                        break;
                    case IngredientType.ZEBRA:
                        curBag = player.zebraBags[v];
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
                    if (player is Player)
                    {
                        MMVibrationManager.Haptic(HapticTypes.LightImpact);
                    //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[5], 1, false);
                    }
                    closet.SpawnOutfit();
                    player.DelayCatch(player.timeDelayCatch);
                    //(player as BaseActor).ShortObj();
                }
            }
        //}
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
