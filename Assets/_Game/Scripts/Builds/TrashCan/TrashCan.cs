using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Transform myTransform;
    public float timeStay;
    private float t;
    private bool throwFood = false;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    private void Start()
    {
        t = timeStay;
        throwFood = false;
    }
    private void OnTriggerEnter(Collider other)
    {
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
            if (!throwFood)
            {
                t -= Time.deltaTime;
            }
            else
            {
                int value = player.objHave - 1;
                if (value < 0)
                    return;
                if (!player.canCatch)
                    return;
                player.objHave--;
                player.canCatch = false;
                var cur = player.allIngredients[value];
                player.RemoveIngredient(cur);
                cur.MoveToTrash(this);
                player.DelayCatch(player.timeDelayCatch);
            }
            if (t < 0)
            {
                throwFood = true;
                t = timeStay;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<ICollect>();
        if (player != null)
        {
            player.canCatch = false;
        }
    }
}
