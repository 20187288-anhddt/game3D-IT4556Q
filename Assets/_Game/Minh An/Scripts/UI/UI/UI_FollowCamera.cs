using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowCamera : MonoBehaviour
{
    public Transform myTranform;
    private Transform cameraMainTransForm;

    public virtual void Awake()
    {
        if (myTranform == null) { myTranform = this.transform; }
        if (cameraMainTransForm == null) { cameraMainTransForm = Camera.main?.transform; }

    }
    public virtual void Update()
    {
        myTranform.LookAt(myTranform.position + cameraMainTransForm.rotation * Vector3.forward /*+ Vector3.forward*/, cameraMainTransForm.rotation * Vector3.up);
    }
    public void SetLocalPosition(Vector3 position)
    {
        myTranform.localPosition = position;
    }
    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public bool isActive()
    {
        return gameObject.activeInHierarchy;
    }
}
