using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class GameManager : MenuManager
{
    [HideInInspector]
    public int buildUnlock;
    public Joystick joystick;
    public List<LevelManager> listLevelManagers;
    public List<TutManager> tutManagers;
    public int curLevel;
    public DataPrice dataPrice;

    public void Start()
    {
        LoadLevelInGame(0);
        Vector3 point_SpawnPlayer = MapController.Instance.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPoint_SpwanPlayer();
        point_SpawnPlayer.y = Player.Instance.myTransform.position.y;
        Player.Instance.myTransform.position = point_SpawnPlayer;
    }

    public void LoadLevelInGame(int levelNum)
    {
        ResetAllLevel();
        foreach (LevelManager levelManager in listLevelManagers)
        {
            //levelManager.gameObject.SetActive(false);
            levelManager.ResetLevel();
        }
        listLevelManagers[levelNum].gameObject.SetActive(true);
        listLevelManagers[levelNum].ResetLevel();
        listLevelManagers[levelNum].StartInGame();
    }
    public void ResetAllLevel()
    {
        //foreach (LevelManager levelManager in listLevelManagers)
        //{
        //    levelManager.gameObject.SetActive(false);
        //}
        //Player.Instance.ResetPlayer();
        //AllPoolContainer.Instance.ReleaseAll();
        //BuildUnitPoolContainer.Instance.ReleaseAll();
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        EnventManager.TriggerEvent(EventName.QuitGame.ToString());
    }
}
