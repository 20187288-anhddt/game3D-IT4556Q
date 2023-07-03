using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextLevel : BaseBuild, ILock
{
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private GameObject cus;
    [SerializeField]
    private GameObject car;
    public Transform startPos;
    public Transform idlePos;
    public GameObject carSmoke;
    [SerializeField] private GameObject checkColliPlayer;
    public float DefaultCoin { get => defaultCoin; }
    public bool IsLock { get => isLock; set => isLock = value; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }

    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        unlockFx.SetActive(true);
        Player p = Player.Instance;
        base.UnLock();
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        p.isUnlock = true;
        if (isPlayAnimUnlock) //anim
        {
            car.transform.DOMove(idlePos.position, 3f).OnComplete(() =>
            {
                //anim.Play("Open");
                carSmoke.gameObject.SetActive(true);
                checkColliPlayer.SetActive(false);
                //checkPushCarMission.GetComponent<BoxCollider>().enabled = true;
            });
            p.isUnlock = false;
        }
        else
        {
            p.isUnlock = false;
        }
        checkUnlock.gameObject.SetActive(false);
        if (isPushEvent)
        {
            // Debug.Log("bbbb");
            //CounterHelper.Instance.QueueAction(2f, () =>
            //{
            //    unlockFx.SetActive(false);
            //});
        }
        EnventManager.TriggerEvent(EventName.StatusData_OnLoad.ToString());
    }
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
           dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        //   Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis() - 1);
        if (isLock)
        {
            checkUnlock.gameObject.SetActive(true);
            //Debug.Log(CurrentCoin);
            if (CurrentCoin <= 0)
            {
                UnLock(false, true);
            }
        }
        checkUnlock.UpdateUI();
    }
}
