using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliPlayerCar : MonoBehaviour
{
    public Transform myTransform;
    public Transform pointRight;
    public Transform pointLeft;

    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    
    private void OnTriggerStay(Collider other)
    {
        //var player = other.GetComponent<Player>();
        //IUnlock player = Cache.getIUnlock(other);
        //if (player != null)
        //{
            Vector3 point = Vector3.zero;
            if (other.transform.position.z <= myTransform.position.z)
            {
                point = pointLeft.position;
                //this.GetComponent<BoxCollider>().isTrigger = false;
            }
            else
            {
                point = pointLeft.position;
                
            }
           
            point.y = other.transform.position.y;
            other.transform.position = point;
        //}
    }
}
