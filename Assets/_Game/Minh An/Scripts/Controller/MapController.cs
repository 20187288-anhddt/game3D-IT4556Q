using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : Singleton<MapController>
{
    [SerializeField] private GameObject Level1;
    [SerializeField] private GameObject Level2;
    [SerializeField] private GameObject Level3;
    private void Start()
    {
        OpenMap(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent());
        NavMesh.RemoveAllNavMeshData();
        string pathNavMesh = "Data_NavMeshAI\\" + "Map" + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Level " + /*value*/3;
        NavMesh.AddNavMeshData((NavMeshData)Resources.Load(pathNavMesh, typeof(NavMeshData)));
    }

    public void OpenMap(int value)
    {
        switch (value)
        {
            case 1:
                Level1.gameObject.SetActive(true);
                Level2.gameObject.SetActive(false);
                Level3.gameObject.SetActive(false);
                break;
            case 2:
                Level1.gameObject.SetActive(false);
                Level2.gameObject.SetActive(true);
                Level3.gameObject.SetActive(false);
                break;
            case 3:
                Level1.gameObject.SetActive(false);
                Level2.gameObject.SetActive(false);
                Level3.gameObject.SetActive(true);
                break;
        }
        if(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent() !=
            value)
        {
            DataManager.Instance.GetDataMap().GetMapCurrent().SetLevelInMapCurrent(value);
        }
        //NavMesh.RemoveAllNavMeshData();
        //string pathNavMesh = "Data_NavMeshAI\\" + "Map" + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Level " + /*value*/3;
        //NavMesh.AddNavMeshData((NavMeshData)Resources.Load(pathNavMesh, typeof(NavMeshData)));
    }
    
}
