
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Utilities.Components;

public class CoinSpawn : MonoBehaviour
{
   // private float t;
    public float timeDelaySpawn;
    public List<Transform> listTrans;
    public Transform transMoney;
    private Vector3 current;
    [SerializeField]
    private Checkout checkOut;

    public Vector3 SpawnObjectOnComplete(int value)
    {
        if(value > 90)
        {
            value = 0;
        }
        int i = value % 9;
        int y = value / 9;
        current = listTrans[i].position;
      //  float x = Random.Range(-0.05f, 0.05f);
       // float z = Random.Range(-0.05f, 0.05f);
        current += Vector3.up * y * 0.35f + Vector3.right * 0 + Vector3.forward * 0;
        return current;
    }
    private void OnTriggerEnter(Collider other)
    {
        //var player = other.GetComponent<Player>();
       // Player player = Player.Instance;
        if (Player.Instance != null)
        {
            Player.Instance.canCatch = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //var player = other.GetComponent<Player>();
       // Player player = Player.Instance;
        //if (player != null)
        //{
        if(checkOut.coins.Count <= 0)
        {
            AudioManager.Instance.StopSFX(AudioCollection.Instance.sfxClips[3]);
        }
        if (!Player.Instance.canCatch || checkOut.coins.Count <= 0)
        {
            return;
        }
        Player.Instance.canCatch = false;
            //if(checkOut.coins.Count < 25)
            //{
            //    checkOut.coins[0].MoveToPlayerSpeed(player,0.1f);
            //}
            //else
            //{
        checkOut.coins[checkOut.coins.Count-1].MoveToPlayerSpeed(Player.Instance);
            //}
        checkOut.coins.Remove(checkOut.coins[checkOut.coins.Count-1]);
        //if(checkOut.coins.Count > 0)
        //{
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[3], 1, false);
        //}
        //else
        //{
           
        //}
        checkOut.coinSave--;
        DataManager.Instance.GetDataMoneyController().AddMoney(Money.TypeMoney.USD, 10);
        Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
        parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
        parameters[1] = new Firebase.Analytics.Parameter("value", 10);
        parameters[2] = new Firebase.Analytics.Parameter("source", "Purchase_In_Map");
        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("earn_virtual_currency", parameters);

        //for (int i = 0; i < checkOut.coins.Count; i++)
        //{
        //    checkOut.coins[i].MoveToPlayerSpeed(player);
        //    checkOut.coins.Remove(checkOut.coins[i]);
        //    DataManager.Instance.GetDataMoneyController().AddMoney(Money.TypeMoney.USD, 10);
        //    i--;
        //    break;
        //}
        Player.Instance.DelayCatch(0.01f);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        AudioManager.Instance.StopSFX(AudioCollection.Instance.sfxClips[3]);
    }
}
