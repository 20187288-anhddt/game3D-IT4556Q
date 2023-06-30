using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildMap : BaseBuild,  ILock
{
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    //[SerializeField]
    //private GameObject unlockModel;
    //[SerializeField]
    //private GameObject lockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
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
        //p.isUnlock = true;
        //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        //unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        //if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        //{
        //    p.myTransform.position = checkUnlock.myTransform.position - Vector3.left * 4;
        //}
        //if (isPlayAnimUnlock) //anim
        //{
        //    unlockModel.transform.DOMoveY(2, 0f).OnComplete(() =>
        //    {
        //        unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() =>
        //        {
        //            unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
        //            {
        //                p.isUnlock = false;
        //                //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
        //            });
        //        }); ;
        //    });
        //}
        //else
        //{
        //    p.isUnlock = false;
        //    //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
        //}
        checkUnlock.gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = true;s
        //levelManager.closetManager.listAllActiveClosets.Add(this);
        switch (nameObject_This)
        {
            case NameObject_This.BuildStage:
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.BuildStage_OnComplete.ToString());
                }
                break;
            case NameObject_This.BuildStage_1:
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.BuildStage_1_OnComplete.ToString());
                }
                break;
        }
    }
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
       // Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis());
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
            dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
       
        if (isLock)
        {
            //GetComponent<BoxCollider>().enabled = false;
            //unlockModel.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            if (CurrentCoin <= 0)
            {
                UnLock(true, true);
            }
        }
        //if (!isLock)
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
    }
}
