using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollect : MonoBehaviour
{
    [SerializeField]
    private Habitat habitat;

    private void OnTriggerEnter(Collider other)
    {
        if (habitat.isLock /*|| habitat.animalsIsReady.Count <= 0*/)
            return;
        var player = other.GetComponent<ICollect>();
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
        var player = other.GetComponent<ICollect>();
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
                    var curIngredient = AllPoolContainer.Instance.Spawn(habitat.ingredientPrefabs, curAnimal.transform);
                    (curIngredient as IngredientBase).MoveToICollect(player);
                    curAnimal.CollectWool();
                    player.DelayCatch(player.timeDelayCatch);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        //if (player != null)
        //{
            player.canCatch = false;
            player.CollectDone();
        //}
    }
}
