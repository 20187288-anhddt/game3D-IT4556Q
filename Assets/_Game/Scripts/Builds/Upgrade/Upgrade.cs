using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var actor = other.GetComponent<Player>();
        if (actor != null)
        {
            //TODO
            UI_Manager.Instance?.OpenUI(NameUI.Canvas_Upgrades);
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    var actor = other.GetComponent<Player>();
    //    if (actor != null)
    //    {
    //        //TODO
    //        UI_Manager.Instance?.CloseUI(NameUI.Canvas_Upgrades);
    //    }
    //}
}
