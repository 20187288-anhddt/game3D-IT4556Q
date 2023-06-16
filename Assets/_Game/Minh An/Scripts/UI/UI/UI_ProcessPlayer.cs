using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ProcessPlayer : UI_FollowCamera
{
    
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
    }
    public void SetPosition(Vector3 position)
    {
        myTranform.position = position;
    }
   
}
