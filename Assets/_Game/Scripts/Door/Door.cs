using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    private void OnTriggerEnter(Collider other)
    {
        var actor = other.GetComponent<IAct>();
        if (actor != null)
        {
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var actor = other.GetComponent<IAct>();
        if (actor != null)
        {
            
        }
    }
}
