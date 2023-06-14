using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireAnimal : BaseBuild, ILock
{
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField] private List<Transform> transPoint_Spawn;
    [SerializeField] private List<Vector3> pointSpawns;
    public float DefaultCoin { get => defaultCoin; }
    public bool IsLock { get => isLock; set => isLock = value; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public bool isHired;
    public override void Awake()
    {
        base.Awake();
        // Debug.Log(dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
        pointSpawns.Clear();
        for (int i = 0; i < transPoint_Spawn.Count; i++)
        {
            pointSpawns.Add(transPoint_Spawn[i].position);
        }
    }
 
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
            habitat.SpawnAnimal(true,new Vector3(this.transform.position.x, habitat.transform.position.y, this.transform.position.z));
            //CounterHelper.Instance.QueueAction(2f, () =>
            //{
            //    unlockFx.SetActive(false);
            //});
        }
        EnventManager.TriggerEvent(EventName.StatusData_OnLoad.ToString());
        StartInGame();
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
        Vector3 pointSpawn = pointSpawns[dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis() - 1];
        pointSpawn.y = myTransform.position.y;
        myTransform.position = pointSpawn;
     //   Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis() - 1);
        if (isLock)
        {
            unlockFx.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            //Debug.Log(CurrentCoin);
            if (CurrentCoin <= 0)
            {
                UnLock(false, true);
               // Debug.Log("a");
            }
        }
        checkUnlock.UpdateUI();
    }
}
