using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_LoadAnim : UI_Canvas
{
    [SerializeField] private Animator anim;
    private static string NameAnimOpen = "Canvas_NextSceneOpen";
    private static string NameAnimClose = "Canvas_NextSceneClose";
    public override void Open()
    {
        gameObject.SetActive(true);
       
    }
    public override void Close()
    {
        gameObject.SetActive(false);
        
    }
    public Animator GetAnim()
    {
        return anim;
    }
    public void PlayAnimOpen()
    {
        anim.Play(NameAnimOpen);
    }
    public void PlayAnimClose()
    {
        anim.Play(NameAnimClose);
    }
}
