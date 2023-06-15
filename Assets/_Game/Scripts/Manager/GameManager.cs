using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class GameManager : MenuManager
{
    public Joystick joystick;
    public List<LevelManager> listLevelManagers;
    public int curLevel;

    public void Start()
    {
        LoadLevelInGame(0);
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
}
