using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public List<Staff> listAllActiveStaffs;
    public List<Staff> listFarmers;
    public List<Staff> listWorkers;
    public List<TrashCan> listTrashCan;
    public GameManager gameManager;
    public Transform[] listIdlePos;
    public bool isChangeIdlePos;

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
        //ChangeStaffIdlePos();
    }
    public void ChangeStaffIdlePos(Staff staff)
    {
        switch (staff.staffType)
        {
            case StaffType.FARMER:
                if(listFarmers.Count == 1)
                {
                    staff.curGarbage = listTrashCan[0];
                    staff.transGarbage = listTrashCan[0].staffPos.position;
                    staff.transIdle = listIdlePos[0].position;
                }
                else if(listFarmers.Count == 2)
                {
                    staff.curGarbage = listTrashCan[1];
                    staff.transGarbage = listTrashCan[1].staffPos.position;
                    staff.transIdle = listIdlePos[1].position;
                }
                break;
            case StaffType.WORKER:
                if (listWorkers.Count == 1)
                {
                    staff.curGarbage = listTrashCan[2];
                    staff.transGarbage = listTrashCan[2].staffPos.position;
                    staff.transIdle = listIdlePos[2].position;
                }
                else if(listWorkers.Count == 2)
                {
                    staff.curGarbage = listTrashCan[3];
                    staff.transGarbage = listTrashCan[3].staffPos.position;
                    staff.transIdle = listIdlePos[3].position;
                }
                break;
        }

    }
    public void CheckFarmerMisson()
    {
        if (listAllActiveStaffs.Count <= 0 || listFarmers.Count <= 0)
            return;

        LevelManager levelManager = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_1);
                break;
            case 1:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_2);
                break;
        }
        for (int i = 0; i < listFarmers.Count; i++)
        {
            if (!listFarmers[i].onMission)
            { 
                Staff curStaff = listFarmers[i];
                MachineBase curMachine = levelManager.machineManager.CheckMachineInputEmtyWithType();
                if (curMachine != null)
                {
                    curStaff.ResetStaff();
                    curStaff.ingredientType = curMachine.ingredientType;
                    Habitat curHabitat = null;
                    curHabitat = levelManager.habitatManager.GetHabitatWithTypeForStaff(curMachine.ingredientType);
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
        LevelManager levelManager = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_1);
                break;
            case 1:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_2);
                break;
        }
        for (int i = 0; i < listWorkers.Count; i++)
        {
            if (!listWorkers[i].onMission)
            {
                Staff curStaff = listWorkers[i];
                int r = Random.Range(0, 2);
                if(r < 1)
                {
                    Closet curCloset = levelManager.closetManager.GetClosetDontHaveOutfit(curStaff.maxCollectObj);
                    if(curCloset != null)
                    {
                        curStaff.ResetStaff();
                        curStaff.ingredientType = curCloset.ingredientType;
                        ClothMachine curClothMachine = null;
                        curClothMachine = levelManager.machineManager.GetClothMachineWithType(curCloset.ingredientType);
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
                    BagCloset curCloset = levelManager.closetManager.GetBagClosetDontHaveBag(curStaff.maxCollectObj);
                    if (curCloset != null)
                    {
                        curStaff.ResetStaff();
                        BagMachine curBagMachine = null;
                        curStaff.ingredientType = curCloset.ingredientType;
                        curBagMachine = levelManager.machineManager.GetBagMachineWithType(curCloset.ingredientType);
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
