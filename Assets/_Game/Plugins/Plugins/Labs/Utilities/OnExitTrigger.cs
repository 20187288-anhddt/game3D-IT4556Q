using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitTrigger : MonoBehaviour
{
    private List<Collider> enterColliders;

    private void OnTriggerEnter(Collider other)
    {
        enterColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enterColliders.Remove(other);
    }

    private void OnDisable()
    {
        
    }

}
