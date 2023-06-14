using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCharacter : MonoBehaviour
{
    public Animator anim;
    public Player player;

    private void UpdateAnim(string state)
    {
        Debug.Log(state);
        if (state == null || anim == null)
            return;
        anim.SetFloat("Speed", player.animSpd);
        if (state == BaseActor.IDLE_STATE)
        {
            if (player.gun.activeSelf)
            {
                anim.Play("IdleWithGun");
            }
            else
            {
                if (player.objHave > 0)
                {
                    anim.Play("IdleCarring");
                }
                else
                    anim.Play("IdleNormal");
            }
        }
        else if (state == BaseActor.RUN_STATE)
        {
            if (player.gun.activeSelf)
            {
                anim.Play("WalkWithGun");
            }
            else
            {
                if (player.objHave > 0)
                {
                    anim.Play("WalkCarring");
                }
                else
                {
                    anim.Play("WalkNormal");
                }              
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAnim(player.STATE_ACTOR);
    }
}
