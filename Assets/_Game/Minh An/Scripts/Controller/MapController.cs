using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    public NameMap nameMap;
    [SerializeField] private List<MiniMapController> miniMaps;

    private void Start()
    {
        OpenMap(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent());
    }

    public void OpenMap(MiniMapController.TypeLevel typeLevel)
    {
        foreach (MiniMapController miniMapController in miniMaps)
        {
            if(miniMapController.typeLevelThis == typeLevel)
            {
                miniMapController.OpenMiniMap();
            }
            else
            {
                miniMapController.CloseMiniMap();
            }
        }
        if(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent() !=
            typeLevel)
        {
            DataManager.Instance.GetDataMap().GetMapCurrent().SetLevelInMapCurrent(typeLevel);
        }
        NavMesh.RemoveAllNavMeshData();
        string pathNavMesh = "Data_NavMeshAI\\" + "Map" + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Level " + (int)typeLevel;
        NavMesh.AddNavMeshData((NavMeshData)Resources.Load(pathNavMesh, typeof(NavMeshData)));
        EnventManager.TriggerEvent(EventName.ReLoadNavMesh.ToString());
        EnventManager.TriggerEvent(EventName.ReLoadDistanceCamera.ToString());
       
    }
    public MiniMapController GetMiniMapController(MiniMapController.TypeLevel typeLevel)
    {
        foreach (MiniMapController miniMapController in miniMaps)
        {
            if (miniMapController.typeLevelThis == typeLevel)
            {
                return miniMapController;
            }
        }
        return null;
    }
}
