using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireStaff : BaseBuild, ILock
{
    public StaffType staffType;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private Checkout curCheckout;
    public float DefaultCoin { get => defaultCoin; }
    public bool IsLock { get => isLock; set => isLock = value; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public Staff staffPrefabs;
    public bool isUnlock;
    public override void Awake()
    {
        base.Awake();
       // Debug.Log(dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
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
        p.isUnlock = true;
        if (isPlayAnimUnlock) //anim
        {
            p.isUnlock = false;
            //unlockModel.transform.DOMoveY(2, 0f).OnComplete(() =>
            //{
            //    unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() =>
            //    {
            //        unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
            //        {
            //            p.isUnlock = false;
            //            //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());

            //            checkPushCloset.gameObject.SetActive(true);
            //        });
            //    }); ;
            //});
        }
        else
        {
            p.isUnlock = false;
        }
        checkUnlock.gameObject.SetActive(false);
        switch (staffType)
        {
            case StaffType.FARMER:
                var curFarmer = AllPoolContainer.Instance.Spawn(staffPrefabs, myTransform.position, myTransform.rotation);
                (curFarmer as Staff).ResetStaff();
                (curFarmer as Staff).staffType = StaffType.FARMER;
                (curFarmer as Staff).ChangeOutfit(StaffType.FARMER);
                levelManager.staffManager.listAllActiveStaffs.Add(curFarmer as Staff);
                levelManager.staffManager.listFarmers.Add(curFarmer as Staff);
                break;
            case StaffType.WORKER:
                var curWorker = AllPoolContainer.Instance.Spawn(staffPrefabs, myTransform.position, myTransform.rotation);
                (curWorker as Staff).ResetStaff();
                (curWorker as Staff).staffType = StaffType.WORKER;
                (curWorker as Staff).ChangeOutfit(StaffType.WORKER);
                levelManager.staffManager.listAllActiveStaffs.Add(curWorker as Staff);
                levelManager.staffManager.listWorkers.Add(curWorker as Staff);
                break;
            case StaffType.CHECKOUT:
                //Checkout curCheckout = GetComponentInParent<Checkout>();
                curCheckout.BuyStaff();
                break;
        }
        levelManager.staffManager.ChangeStaffIdlePos();
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
        if (isLock)
        {
            checkUnlock.gameObject.SetActive(true);
        }
        checkUnlock.UpdateUI();
    }
}
