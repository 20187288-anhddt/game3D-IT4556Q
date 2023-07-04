using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Utilities.Components;
using MoreMountains.NiceVibrations;

public class GameManager : MenuManager
{
    [HideInInspector]
    public int buildUnlock;
    public Joystick joystick;
    public Image[] joystickImage;
    public List<LevelManager> listLevelManagers;
    public List<TutManager> tutManagers;
    public int curLevel;
    public DataPrice dataPrice;
    public bool IsMusic { get; set; }
    public bool IsSound { get; set; }
    public bool IsVibrate { get; set; }
    public bool IsJoystick { get; set; }
   
    public void Start()
    {
        LoadLevelInGame(0);
        Vector3 point_SpawnPlayer = MapController.Instance.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPoint_SpwanPlayer();
        point_SpawnPlayer.y = Player.Instance.myTransform.position.y;
        Player.Instance.myTransform.position = point_SpawnPlayer;
        MMVibrationManager.SetHapticsActive(true);
        AudioManager.Instance.EnableMusic(true);
        AudioManager.Instance.EnableSFX(true);
        AudioManager.Instance.PlayMusic(AudioCollection.Instance.musicClips[0], true, 0, 0.5f);
        //if (IsJoystick)
        //{
        //    //Hien thi joystick
        //}
        //else
        //{
        //    //An joystick
        //}
        if (listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].dataLevelManager.Get_isDoneAllTUT())
        {
            SDK.AdsManager.Instance._ShowBannerAds();
        }
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
        curLevel = levelNum;
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
    public void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            EnventManager.TriggerEvent(EventName.QuitGame.ToString());
            if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].dataLevelManager.Get_isDoneAllTUT())
            {
                EnventManager.TriggerEvent(EventName.ClearData.ToString());
                Dictionary<string, string> pairs_ = new Dictionary<string, string>();
                pairs_.Add("af_success", "false");
                pairs_.Add("af_tutorial_id", DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap.ToString());
                SDK.ABIAppsflyerManager.SendEvent("af_tutorial_completion", pairs_);
                //  DataManager.Instance.ClearAllData();
            }
        }
    }
    public void OnApplicationPause(bool pause)
    {
        if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].dataLevelManager.Get_isDoneAllTUT()/* &&*/
              /*!AppOpenAdManager.Instance.IsShowingAd()*/)
        {
            Application.Quit();
        }
        else
        {
            EnventManager.TriggerEvent(EventName.QuitGame.ToString());
        }
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        EnventManager.TriggerEvent(EventName.QuitGame.ToString());
        if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].dataLevelManager.Get_isDoneAllTUT())
        {
            EnventManager.TriggerEvent(EventName.ClearData.ToString());
            Dictionary<string, string> pairs_ = new Dictionary<string, string>();
            pairs_.Add("af_success", "false");
            pairs_.Add("af_tutorial_id", DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap.ToString());
            SDK.ABIAppsflyerManager.SendEvent("af_tutorial_completion", pairs_);
            //  DataManager.Instance.ClearAllData();
        }
    }
    public void EnableJoystick(bool on)
    {
        if (on)
        {
            foreach (Image i in joystickImage)
            {
                i.enabled = true;
            }
            IsJoystick = true;
        }
        else
        {
            foreach (Image i in joystickImage)
            {
                i.enabled = false;
            }
            IsJoystick = false;
        }
    }
    public void SoundSwitch(bool on)
    {
        if (on)
        {
            IsMusic = true;
            IsSound = true;
            AudioManager.Instance.EnableMusic(IsMusic);
            AudioManager.Instance.EnableSFX(IsSound);
        }
        else
        {
            IsMusic = false;
            IsSound = false;
            AudioManager.Instance.EnableMusic(IsMusic);
            AudioManager.Instance.EnableSFX(IsSound);
        }
    }
    public void VibrationSwitch(bool on)
    {
        if (on)
        {
            MMVibrationManager.SetHapticsActive(true);
            IsVibrate = true;
        }
        else
        {
            MMVibrationManager.SetHapticsActive(false);
            IsVibrate = false;
        }
    }
}
