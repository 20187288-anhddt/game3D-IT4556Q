using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireAnimal : BaseBuild, ILock
{
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public float DefaultCoin { get => defaultCoin; }
    public bool IsLock { get => isLock; set => isLock = value; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public bool isHired;
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
        }
        else
        {
            p.isUnlock = false;
        }
        checkUnlock.gameObject.SetActive(false);
        if (!isHired)
        {
            habitat.SpawnAnimal(true);
        }
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
