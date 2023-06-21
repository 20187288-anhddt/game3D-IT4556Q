using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoBuild : UI_FollowCamera
{
    [SerializeField] private Text txt_Process;

    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
    }
    public void LoadTextProcess(string text)
    {
        txt_Process.text = text;
    }
}
