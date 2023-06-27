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
            //Show pop-up
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var actor = other.GetComponent<Player>();
        if (actor != null)
        {
            //TODO
            //Hide pop-up
        }
    }
}
