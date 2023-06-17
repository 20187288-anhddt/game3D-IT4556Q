
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Utilities.Components;

public class CoinSpawn : MonoBehaviour
{
    private float t;
    public float timeDelaySpawn;
    public List<Transform> listTrans;
    public Transform transMoney;
    private Vector3 current;
    [SerializeField]
    private Checkout checkOut;

    public Vector3 SpawnObjectOnComplete(int value)
    {
        if(value > 27)
        {
            value = 0;
        }
        int i = value % 9;
        int y = value / 9;
        current = listTrans[i].position;
        float x = Random.Range(-0.05f, 0.05f);
        float z = Random.Range(-0.05f, 0.05f);
        current += Vector3.up * y * 0.35f + Vector3.right * x + Vector3.forward * z;
        return current;
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            if (!player.canCatch || checkOut.coins.Count <= 0)
                return;
            player.canCatch = false;
            for (int i = 0; i < checkOut.coins.Count; i++)
            {
                checkOut.coins[i].MoveToPlayerSpeed(player);
                checkOut.coins.Remove(checkOut.coins[i]);
                i--;
            }
        }
    }

}
