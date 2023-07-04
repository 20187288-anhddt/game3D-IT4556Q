using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;

public class CheckCollect : MonoBehaviour
{
    [SerializeField]
    private Habitat habitat;

    private void OnTriggerEnter(Collider other)
    {
        if (habitat.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        //if (player != null)
        //{
            if(player is Player)
            {
                player.canCatch = true;
                player.Collect();
            }
            if(player is Staff)
            {
                if ((player as Staff).ingredientType == habitat.ingredientType)
                {
                    player.canCatch = true;
                    player.Collect();
                } ;
            }
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (habitat.isLock)
            return;
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        if (/*player != null && */player.objHave < player.maxCollectObj)
        {
            if (player is Staff)
            {
                if ((player as Staff).staffType != StaffType.FARMER || (player as Staff).ingredientType != habitat.ingredientType)
                {
                    return;
                };
            }
            if (!player.canCatch)
                return;
            if (habitat.listShit.Count > 0)
            {
                int value = habitat.listShit.Count - 1;
                if (value < 0)
                    return;
                player.canCatch = false;
                var curShit = habitat.listShit[value];
                (curShit as IngredientBase).MoveToICollect(player);
                habitat.listShit.Remove(curShit as Shit);
                habitat.numShitSave--;
                player.DelayCatch(player.timeDelayCatch);
            }
            else
            {
                if (habitat.animalsIsReady.Count > 0)
                {
                    int value = habitat.animalsIsReady.Count - 1;
                    if (value < 0)
                        return;
                    player.canCatch = false;
                    var curAnimal = habitat.animalsIsReady[value];
                    curAnimal.CollectWool();
                    switch (habitat.ingredientType)
                    {
                        case IngredientType.COW:
                            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[2], 1, false);
                            break;
                        case IngredientType.BEAR:
                            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[0], 1, false);
                            break;
                        case IngredientType.CHICKEN:
                            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[1], 1, false);
                            break;
                    }
                    var curIngredient = AllPoolContainer.Instance.Spawn(habitat.ingredientPrefabs, curAnimal.transform);
                    (curIngredient as IngredientBase).MoveToICollect(player);
                   
                    player.DelayCatch(player.timeDelayCatch);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //var player = other.GetComponent<ICollect>();
        var player = Cache.getICollect(other);
        //if (player != null)
        //{
        player.canCatch = false;
        player.CollectDone();
        //}
    }
}
