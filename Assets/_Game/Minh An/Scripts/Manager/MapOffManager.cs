using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOffManager : GenerticSingleton<MapOffManager>
{
    [SerializeField] private List<MapOffController> mapOffControllers = new List<MapOffController>();
    [SerializeField] private Dictionary<int, int> keyValuePairs_CountUpgradeInMap = new Dictionary<int, int>();
    private static string NameDataCountUpgradeInMap = "DataCountUpgradeInMap_";
    private int IDMapMax = 0;
    private int idMapCurrent;
  
    private void OnInIt()
    {
        IDMapMax = LoadMapController.Instance.GetCountMaxMap();
        idMapCurrent = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1;
        LoadDataAllCountUpgradeMap();
        SpawnMapOffMoney();
    }
    private void Start()
    {
        AddEvent();
        Invoke(nameof(OnInIt), 0.1f);
    }
    private void AddEvent()
    {
        EnventManager.AddListener(EventName.UpdateDataCountUpgradeCurrent.ToString(), LoadAndSaveDataCountUpgradeMapCurrent);
    }
    private void SpawnMapOffMoney()
    {
        for(int i = 0; i < IDMapMax; i++)
        {
            GameObject objMap = new GameObject();
            objMap.transform.SetParent(this.transform);
            objMap.name = "Map " + (i + 1);
            MapOffController mapController = objMap.AddComponent<MapOffController>();
            mapOffControllers.Add(mapController);
            mapController.Load(i, IsMapUnlock(i), IsMapCurrent(i));
        }
    }
    public void ReLoadMapOffMoney()
    {
        foreach(MapOffController mapOffController in mapOffControllers)
        {
            mapOffController.Load(mapOffController.GetIDMap(), IsMapUnlock(mapOffController.GetIDMap()), IsMapCurrent(mapOffController.GetIDMap()));
        }
    }
    private void LoadAndSaveDataCountUpgradeMapCurrent()
    {
       
        int countUpgrade = 0;
        countUpgrade = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetAllLevel();
        var _enum = System.Enum.GetValues(typeof(StaffType));
        LevelManager levelManager = null;
        switch (idMapCurrent)
        {
            case 0:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_1);
                break;
            case 1:
                levelManager = GameManager.Instance.GetLevelManager(NameMap.Map_2);
                break;
        }
        foreach (StaffType staffType in _enum)
        {
            DataStaff dataStaff = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType);
            if (dataStaff != null)
            {
                countUpgrade += dataStaff.GetAllLevel();
            }
           
        }

        foreach (MachineBase machineBase in levelManager.machineManager.allActiveMachine)
        {
            MachineDataStatusObject machineDataStatusObject = (machineBase.dataStatusObject as MachineDataStatusObject);
            countUpgrade += machineDataStatusObject.GetLevel_Speed() + machineDataStatusObject.GetLevel_Stack() - 2;
        }
        if (!keyValuePairs_CountUpgradeInMap.ContainsKey(idMapCurrent))
        {
            keyValuePairs_CountUpgradeInMap.Add(idMapCurrent, countUpgrade);
        }
        else
        {
            keyValuePairs_CountUpgradeInMap[idMapCurrent] = countUpgrade;
        }

        //Save
        PlayerPrefs.SetInt(NameDataCountUpgradeInMap + idMapCurrent, countUpgrade);

    }
    private void LoadDataAllCountUpgradeMap()
    {
        LoadAndSaveDataCountUpgradeMapCurrent();

        for (int i = 0; i < IDMapMax; i++)
        {
            if (idMapCurrent == i)
            {
                continue;
            }
            else
            {
                keyValuePairs_CountUpgradeInMap.Add(i, PlayerPrefs.GetInt(NameDataCountUpgradeInMap + i));
            }
        }
    }
    public int GetCountUpgradeInMap(int ID)
    {
        return keyValuePairs_CountUpgradeInMap[ID];
    }
    public List<MapOffController> GetMapOffControllers()
    {
        return mapOffControllers;
    }
    public MapOffController GetMapOffControllerCurrent()
    {
        return mapOffControllers[idMapCurrent];
    }
    public MapOffController GetMapOffController(int ID)
    {
        if (mapOffControllers.Count > ID)
        {
            return mapOffControllers[ID];
        }
        return null;
    }
    public bool IsMapUnlock(int IDMap)
    {
       // Debug.Log(LoadMapController.Instance.GetIDMapOpenMax());
        if(LoadMapController.Instance.GetIDMapOpenMax() > IDMap)
        {
            return true;
        }
        return false;
    }
    public bool IsMapCurrent(int IDMap)
    {
        if(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1 == IDMap)
        {
            return true;
        }
        return false;
    }
}
