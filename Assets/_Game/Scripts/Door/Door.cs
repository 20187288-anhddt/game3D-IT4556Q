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
    private bool isOpen;
    private List<IAct> actorInDoor;

    void Start()
    {
        actorInDoor = new List<IAct>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //var actor = other.GetComponent<IAct>();
        var actor = Cache.getIAct(other);
        //if (actor != null)
        //{
            if (!actorInDoor.Contains(actor))
            {
                actorInDoor.Add(actor);
            }
            if(actorInDoor.Count == 1)
            {
                leftDoor.transform.DOLocalMoveZ(-4f, 0.5f);
                rightDoor.transform.DOLocalMoveZ(+4f, 0.5f);
            }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //var actor = other.GetComponent<IAct>();
        var actor = Cache.getIAct(other);
        //if (actor != null)
        //{
        if (actorInDoor.Contains(actor))
            {
                actorInDoor.Remove(actor);
            }
            if (actorInDoor.Count == 0)
            {
                leftDoor.transform.DOLocalMoveZ(0f, 0.5f);
                rightDoor.transform.DOLocalMoveZ(0f, 0.5f);
            }
        //}
    }
}
