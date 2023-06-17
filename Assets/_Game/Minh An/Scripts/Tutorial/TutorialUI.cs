using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private static string Name_DataIsTutorialUI = "DataIsTutorialUI";
    [SerializeField] private bool isResetDataOpen;
    private bool isCompleteTuTorialUI;
   
    private void Awake()
    {
        PlayerPrefs.SetInt(Name_DataIsTutorialUI, 0);
        isCompleteTuTorialUI = PlayerPrefs.GetInt(Name_DataIsTutorialUI) == 1;
      
        if (!isCompleteTuTorialUI)
        {
            if (DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoCapacityTaget().infoBuys[0].value != 0)
            {
                SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("checkpoint_03", "tutorial_end", 1);
                Dictionary<string, string> pairs_ = new Dictionary<string, string>();
                pairs_.Add("af_success", "true");
                pairs_.Add("af_tutorial_id", "Tutorial_UI");
                SDK.ABIAppsflyerManager.SendEvent("af_tutorial_completion", pairs_);
                PlayerPrefs.SetInt(Name_DataIsTutorialUI, 1);
                isCompleteTuTorialUI = true;
#if UNITY_EDITOR
                Debug.Log("Complete Tutorial UI");
#endif
            }
        }
//#if UNITY_EDITOR
//        isCompleteTuTorialUI = !isResetDataOpen;
//#endif
    }
    void Update()
    {
        CheckTutorialUpgrade();
    }
    private void CheckTutorialUpgrade()
    {
        if (!isCompleteTuTorialUI)
        {
            TutManager tutManager = null;
            switch (GameManager.Instance.curLevel)
            {
                case 0:
                    tutManager = GameManager.Instance.GetTutManager(NameMap.Map_1);
                    break;
                case 1:
                    tutManager = GameManager.Instance.GetTutManager(NameMap.Map_2);
                    break;
            }
            if (tutManager.IsDoneAllTuT())
            {
                if (UI_Manager.Instance.isCloseUI(NameUI.Canvas_Upgrades) && UI_Manager.Instance.isOpenUI(NameUI.Canvas_Home) && UI_Manager.Instance.isCloseUI(NameUI.Canvas_Tutorial))
                {
                    Canvas_Tutorial canvas_Tutorial = UI_Manager.Instance.OpenUI(NameUI.Canvas_Tutorial) as Canvas_Tutorial;
                    canvas_Tutorial.LoadTutorial(TypeTutorial.Upgrade, () => 
                    {
                        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("checkpoint_03", "tutorial_end", 1);
                        Dictionary<string, string> pairs_ = new Dictionary<string, string>();
                        pairs_.Add("af_success", "true");
                        pairs_.Add("af_tutorial_id", "Tutorial_UI");
                        SDK.ABIAppsflyerManager.SendEvent("af_tutorial_completion", pairs_);
                        PlayerPrefs.SetInt(Name_DataIsTutorialUI, 1);
                        isCompleteTuTorialUI = true;

                        (UI_Manager.Instance.GetUI(NameUI.Canvas_Upgrades) as Canvas_Upgrades).SetActionClose(() =>
                        {
                            EnventManager.TriggerEvent(EventName.CompleteTut_Upgrade.ToString());
                            SDK.AdsManager.Instance._ShowBannerAds();
                        });
                       
                    });
                }
            }
        }
       
    }
}
