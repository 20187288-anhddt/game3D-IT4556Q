using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapController : GenerticSingleton<LoadMapController>
{
    [SerializeField] private Transform transCar;
    [SerializeField] private Vector3 pointStart;
    [SerializeField] private Vector3 pointEnd;
    [SerializeField] private Canvas_LoadAnim canvas_LoadAnim;
    [SerializeField] private GameObject obj_Animation;
    [Header("Prefabs")]
    [SerializeField] private List<Map_> map_Prefabs;
    private Map_ map_Current;
    private Dictionary<int, Map_> map_Loads = new Dictionary<int, Map_>();
    public int indexMapTaget = -1;
    private static string NameDataIDMapOpenMax = "DataIDMapOpenMax";
    private bool isLoadMap = false;
    private void Start()
    {
        //LoadLevel();
        // canvas_LoadAnim.Close();
        if(canvas_LoadAnim == null) { canvas_LoadAnim = UI_Manager.Instance.GetUI(NameUI.Canvas_LoadAnim) as Canvas_LoadAnim; }
        //StartCoroutine(DeLayAction(() =>
        //{
        //    LoadMapNext();
        //}, 10));
        EnventManager.AddListener(EventName.On_NextMap.ToString(), LoadMapNext);
        EnventManager.AddListener(EventName.On_BackMap.ToString(), LoadMapBack);
    }
    private void Update()
    {
        if (isLoadMap)
        {
            if (UI_Manager.Instance.isOpen())
            {
                UI_Manager.Instance.Close();
            }
        }
    }
    public void LoadLevel(int levelLoad = 0, TypeMove typeMove = TypeMove.MoveToMapNext)
    {
        if (indexMapTaget == levelLoad)
        {
            return;
        }
        isLoadMap = true;
        GameManager.Instance.curLevel = levelLoad;
        DataManager.Instance.GetDataProcessInMapController().InItData();
        if (levelLoad + 1 > PlayerPrefs.GetInt(NameDataIDMapOpenMax, 1))
        {
            PlayerPrefs.SetInt(NameDataIDMapOpenMax, levelLoad + 1);
        }
        indexMapTaget = levelLoad;
        if (map_Current != null)
        {
            map_Current.CloseMap();
        }
        DataManager.Instance.GetDataMap().SelectDataMap(indexMapTaget + 1);
       
        if (!map_Loads.ContainsKey(levelLoad))
        {
            map_Current = Instantiate(map_Prefabs[levelLoad]);
            map_Current.CloseMap();
            map_Loads.Add(levelLoad, map_Current);

        }
        else
        {
            map_Current = map_Loads[levelLoad];
        }
        canvas_LoadAnim.Open();
        canvas_LoadAnim.PlayAnimOpen();
        obj_Animation.SetActive(true);
        Vector3 pointStart = Vector3.zero;
        Vector3 pointEnd = Vector3.zero;
        switch (typeMove)
        {
            case TypeMove.MoveToMapNext:
                transCar.localScale = Vector3.one;
                pointStart = this.pointStart;
                pointEnd = this.pointEnd;
                break;
            case TypeMove.MoveToMapBack:
                transCar.localScale = Vector3.right * -1 + Vector3.up + Vector3.forward;
                pointStart = this.pointEnd;
                pointEnd = this.pointStart;
                break;
        }
        UI_Manager.Instance.Close();
        StartCoroutine(AnimCarStartMove(pointStart, pointEnd, 0.2f, () => 
        {
            StartCoroutine(DeLayAction(() => 
            {
                isLoadMap = false;
                canvas_LoadAnim.PlayAnimOpen();
                obj_Animation.SetActive(false);
                EnventManager.TriggerEvent(EventName.LoadMap_Complete.ToString());
                switch (typeMove)
                {
                    case TypeMove.MoveToMapNext:
                        EnventManager.TriggerEvent(EventName.Complete_NextMap.ToString());
                        break;
                    case TypeMove.MoveToMapBack:
                        EnventManager.TriggerEvent(EventName.Complete_BackMap.ToString());
                        break;
                }
                MapController mapController = null;
                switch (GameManager.Instance.curLevel)
                {
                    case 0:
                        mapController = GameManager.Instance.GetMapController(NameMap.Map_1);
                        break;
                    case 1:
                        mapController = GameManager.Instance.GetMapController(NameMap.Map_2);
                        break;
                }
                Vector3 point_SpawnPlayer = mapController.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPoint_SpwanPlayer();
                point_SpawnPlayer.y = Player.Instance.myTransform.position.y;
                Player.Instance.myTransform.position = point_SpawnPlayer;
                UI_Manager.Instance.Open();
                map_Current.OpenMap();
            }, 1));
           
        }, 0));
        Player.Instance.ResetPlayer();
    }
    public void LoadLevel(int levelLoad = 0)
    {
        if(indexMapTaget == levelLoad)
        {
            return;
        }
        indexMapTaget = levelLoad;
        if (map_Current != null)
        {
            map_Current.CloseMap();
        }
        if (!map_Loads.ContainsKey(levelLoad))
        {
            map_Current = Instantiate(map_Prefabs[levelLoad]);
            map_Loads.Add(levelLoad, map_Current);
        }
        else
        {
            map_Current = map_Loads[levelLoad];
        }
        DataManager.Instance.GetDataMap().SelectDataMap(indexMapTaget + 1);
        //map_Current = Instantiate(map_Prefabs[levelLoad]);
        map_Current.OpenMap();
        Player.Instance.ResetPlayer();
    }
    public void LoadMapNext()
    {
        LoadLevel(indexMapTaget + 1, TypeMove.MoveToMapNext);
        AllPoolContainer.Instance.ReleaseAll();
       // Player.Instance.ResetPlayer();
    }
    public void LoadMapBack()
    {
        LoadLevel(indexMapTaget - 1, TypeMove.MoveToMapBack);
        AllPoolContainer.Instance.ReleaseAll();
      //  Player.Instance.ResetPlayer();
    }
    IEnumerator AnimCarStartMove(Vector3 pointStart, Vector3 pointEnd, float speedMove, System.Action actionEnd, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        float m_time = 0;
        pointStart.y = transCar.localPosition.y;
        transCar.localPosition = pointStart;
      
        while(m_time < 1f)
        {
            if(m_time > 0.8f)
            {
                canvas_LoadAnim.Open();
                canvas_LoadAnim.PlayAnimClose();
                //canvas_LoadAnim.Close();
            }
            transCar.localPosition = Vector3.Lerp(pointStart, pointEnd, m_time);
            m_time += Time.deltaTime * speedMove;
            yield return null;
        }
        actionEnd?.Invoke();
    }
    IEnumerator DeLayAction(System.Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }
    public enum TypeMove
    {
        MoveToMapNext,
        MoveToMapBack,
    }
    public int GetCountMaxMap()
    {
        return map_Prefabs.Count;
    }
    public int GetIDMapOpenMax()
    {
        return PlayerPrefs.GetInt(NameDataIDMapOpenMax, 1);
    }
    public Map_ GetMap_()
    {
        return map_Current;
    }
}
public enum NameMap
{
    Map_1,
    Map_2
}