using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public List<Staff> listAllActiveStaffs;
    public List<Staff> listFarmers;
    public List<Staff> listWorkers;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void CheckFarmerMisson()
    {
        if (listAllActiveStaffs.Count <= 0)
            return;
        for(int i = 0; i < listFarmers.Count; i++)
        {
            if (!listFarmers[i].onMission)
            {
                Staff curStaff = listFarmers[i];
                MachineBase curMachine = gameManager.listLevelManagers[gameManager.curLevel].machineManager.CheckMachineInputEmty();
                if (curMachine != null)
                {
                    Habitat curHabitat = null;
                    switch (curMachine.ingredientType)
                    {
                        case IngredientType.SHEEP:
                            curHabitat = gameManager.listLevelManagers[gameManager.curLevel].habitatManager.GetHabitatWithType(IngredientType.SHEEP);
                            break;
                        case IngredientType.COW:
                            curHabitat = gameManager.listLevelManagers[gameManager.curLevel].habitatManager.GetHabitatWithType(IngredientType.COW);
                            break;
                        case IngredientType.CHICKEN:
                            curHabitat = gameManager.listLevelManagers[gameManager.curLevel].habitatManager.GetHabitatWithType(IngredientType.CHICKEN);
                            break;
                        case IngredientType.BEAR:
                            curHabitat = gameManager.listLevelManagers[gameManager.curLevel].habitatManager.GetHabitatWithType(IngredientType.BEAR);
                            break;
                    }
                    if (curHabitat != null)
                    {
                        curStaff.transHabitat = curHabitat.staffPos.transform.position;
                        curStaff.transMachine = curMachine.inStaffPos.transform.position;
                        curStaff.onMission = true;
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
        if (listAllActiveStaffs.Count <= 0)
            return;
        for (int i = 0; i < listWorkers.Count; i++)
        {
            if (!listWorkers[i].onMission)
            {
                Staff curStaff = listWorkers[i];
                int r = Random.Range(0, 1);
                if(r < 0.5)
                {
                    Closet curCloset = gameManager.listLevelManagers[gameManager.curLevel].closetManager.GetClosetDontHaveOutfit();
                    if(curCloset != null)
                    {
                        ClothMachine curClothMachine = null;
                        curClothMachine = gameManager.listLevelManagers[gameManager.curLevel].machineManager.GetClothMachineWithType(curCloset.ingredientType);
                        if(curClothMachine != null)
                        {
                            curStaff.transCloset = curCloset.staffPos.position;
                            curStaff.transMachine = curClothMachine.outStaffPos.position;
                            curCloset.isHaveStaff = true;
                            curClothMachine.isHaveOutStaff = true;
                            curStaff.UpdateState(BaseStaff.MOVE_TO_MACHINE_STATE);
                        }
                    }
                }
            }
        }
    }
    
}
