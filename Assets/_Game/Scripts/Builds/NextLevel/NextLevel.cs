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
        car.SetActive(true);
        unlockFx.SetActive(true);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        p.isUnlock = true;
        if (isPlayAnimUnlock) //anim
        {
            p.PlayerStopMove();
            car.transform.DOMoveY(2, 0f).OnComplete(() => {
                car.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    car.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.PlayerContinueMove();
                        p.isUnlock = false;
                        unlockFx.SetActive(false);
                        carSmoke.gameObject.SetActive(true);
                        //checkColliPlayer.gameObject.SetActive(true);
                        //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                    });
                }); ;
            });
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
            EnventManager.TriggerEvent(EventName.NextLevel_2_Complete.ToString());
        }
        //EnventManager.TriggerEvent(EventName.StatusData_OnLoad.ToString());
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
            car.SetActive(false);
            unlockFx.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            carSmoke.gameObject.SetActive(false);
            //checkColliPlayer.gameObject.SetActive(false);
            //Debug.Log(CurrentCoin);
            if (CurrentCoin <= 0)
            {
                UnLock(true, true);
            }
        }
        checkUnlock.UpdateUI();
    }
}
