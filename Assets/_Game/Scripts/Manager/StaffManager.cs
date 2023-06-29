using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : Singleton<StaffManager>
{
    public List<Staff> listAllActiveStaffs;
    public List<Staff> listFarmers;
    public List<Staff> listWorkers;
    public List<TrashCan> listTrashCan;
    public GameManager gameManager;
    public Transform[] listIdlePos;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        if( listAllActiveStaffs.Count > 0)
        {
            if(listFarmers.Count > 0)
            {
                CheckFarmerMisson();
            }
        }
        if (listAllActiveStaffs.Count > 0)
        {
            if (listWorkers.Count > 0)
            {
                CheckWorkerMission();
            }
        }
    }
    public void ChangeStaffIdlePos()
    {
        for(int i = 0; i< listAllActiveStaffs.Count; i++)
        {
            switch (listAllActiveStaffs[i].staffType)
            {
                case StaffType.FARMER:
                    listAllActiveStaffs[i].curGarbage = listTrashCan[0];
                    listAllActiveStaffs[i].transGarbage = listTrashCan[0].staffPos.position;
                    if (gameManager.listLevelManagers[gameManager.curLevel].habitatManager.allActiveHabitats.Count < 3)
                    {
                        listAllActiveStaffs[i].transIdle = listIdlePos[0].position;
                    }
                    else
                    {
                        listAllActiveStaffs[i].transIdle = listIdlePos[1].position;
                    }
                    break;
                case StaffType.WORKER:
                    listAllActiveStaffs[i].curGarbage = listTrashCan[1];
                    listAllActiveStaffs[i].transGarbage = listTrashCan[1].staffPos.position;
                    if (gameManager.listLevelManagers[gameManager.curLevel].closetManager.listClosets.Count < 3)
                    {
                        listAllActiveStaffs[i].transIdle = listIdlePos[2].position;
                    }
                    else
                    {
                        listAllActiveStaffs[i].transIdle = listIdlePos[3].position;
                    }
                    break;
            }
        }
       
    }
    public void CheckFarmerMisson()
    {
        if (listAllActiveStaffs.Count <= 0 || listFarmers.Count <= 0)
            return;
        for(int i = 0; i < listFarmers.Count; i++)
        {
            if (!listFarmers[i].onMission)
            { 
                Staff curStaff = listFarmers[i];
                MachineBase curMachine = gameManager.listLevelManagers[gameManager.curLevel].machineManager.CheckMachineInputEmtyWithType();
                if (curMachine != null)
                {
                    curStaff.ResetStaff();
                    curStaff.ingredientType = curMachine.ingredientType;
                    Habitat curHabitat = null;
                    curHabitat = gameManager.listLevelManagers[gameManager.curLevel].habitatManager.GetHabitatWithType(curMachine.ingredientType);
                    if (curHabitat != null)
                    { 
                        curStaff.onMission = true;
                        curStaff.curMachine = curMachine;
                        curStaff.curHabitat = curHabitat;
                        curStaff.transHabitat = curHabitat.staffPos.transform.position;
                        curStaff.transMachine = curMachine.inStaffPos.transform.position; 
                        curHabitat.isHaveStaff = true;
                        curMachine.isHaveInStaff = true;
                        curStaff.UpdateState(BaseStaff.MOVE_TO_HABITAT_STATE);
                    }
                }
            }
        }
    }
    public void CheckWorkerMission()
    {
        if (listAllActiveStaffs.Count <= 0 || listWorkers.Count <= 0)
            return;
        for (int i = 0; i < listWorkers.Count; i++)
        {
            if (!listWorkers[i].onMission)
            {
                Staff curStaff = listWorkers[i];
                int r = Random.Range(0, 2);
                if(r < 1)
                {
                    Closet curCloset = gameManager.listLevelManagers[gameManager.curLevel].closetManager.GetClosetDontHaveOutfit(curStaff.maxCollectObj);
                    if(curCloset != null)
                    {
                        curStaff.ResetStaff();
                        curStaff.ingredientType = curCloset.ingredientType;
                        ClothMachine curClothMachine = null;
                        curClothMachine = gameManager.listLevelManagers[gameManager.curLevel].machineManager.GetClothMachineWithType(curCloset.ingredientType);
                        if(curClothMachine != null)
                        {
                            curStaff.onMission = true;
                            curStaff.curCloset = curCloset;
                            curStaff.curMachine = curClothMachine;
                            curStaff.transCloset = curCloset.staffPos.position;
                            curStaff.transMachine = curClothMachine.outStaffPos.position;
                            curCloset.isHaveStaff = true;
                            curClothMachine.isHaveOutStaff = true;
                            curStaff.UpdateState(BaseStaff.MOVE_TO_MACHINE_STATE);
                        }
                    }
                }
                else
                {
                    BagCloset curCloset = gameManager.listLevelManagers[gameManager.curLevel].closetManager.GetBagClosetDontHaveBag(curStaff.maxCollectObj);
                    if (curCloset != null)
                    {
                        curStaff.ResetStaff();
                        BagMachine curBagMachine = null;
                        curStaff.ingredientType = curCloset.ingredientType;
                        curBagMachine = gameManager.listLevelManagers[gameManager.curLevel].machineManager.GetBagMachineWithType(curCloset.ingredientType);
                        if (curBagMachine != null)
                        {
                            curStaff.onMission = true;
                            curStaff.curCloset = curCloset;
                            curStaff.curMachine = curBagMachine;
                            curStaff.transCloset = curCloset.staffPos.position;
                            curStaff.transMachine = curBagMachine.outStaffPos.position;
                            curCloset.isHaveStaff = true;
                            curBagMachine.isHaveOutStaff = true;
                            curStaff.UpdateState(BaseStaff.MOVE_TO_MACHINE_STATE);
                        }
                    }
                }
            }
        }
    }
    
}
