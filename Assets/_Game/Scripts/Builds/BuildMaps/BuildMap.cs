using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildMap : BaseBuild,  ILock
{
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    [SerializeField]
    private GameObject unlockModel;
    //[SerializeField]
    //private GameObject lockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        p.isUnlock = true;
        //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        //if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        //{
        //    p.myTransform.position = checkUnlock.myTransform.position - Vector3.left * 4;
        //}
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() =>
            {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() =>
                {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                        //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
            //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
        }
        checkUnlock.gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = true;
        //levelManager.closetManager.listAllActiveClosets.Add(this);
    }
}
