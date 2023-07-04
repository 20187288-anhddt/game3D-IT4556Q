using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutManager : MonoBehaviour
{
    public List<NameTuT> nameTuTs_Taget;
    private LineRenderer line;
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private ClothMachine clothMachine;
    [SerializeField]
    private BagMachine bagMachine;
    [SerializeField]
    private Closet closet;
    [SerializeField]
    private BagCloset bagCloset;
    [SerializeField]
    private Checkout checkOut;
    [SerializeField]
    private Vector3 checkOutPos;
    [SerializeField]
    private Vector3 pushClothPos;
    [SerializeField]
    private Vector3 collectClothPos;
    private Player player;
    private LevelManager levelManager;
    [SerializeField]
    private GameObject fxTUT;
    public bool isBuildTest;

    void Start()
    {
        levelManager = GameManager.Instance.listLevelManagers[0]; 
        player = Player.Instance;
        line = GetComponentInChildren<LineRenderer>();
        //line.material.SetTextureScale("_MainTex", new Vector2(1,1));
        line.widthMultiplier = 1f;
        line.positionCount = 2;
        fxTUT.SetActive(false);
        player.tutPanel.SetActive(false);
        bool isCompleteTuT = CheckDoneAllTuT();
        if (!isCompleteTuT)
        {
            SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("checkpoint_01", "tutorial_start", 1);
        }
        //EnventManager.AddListener(EventName.QuitGame.ToString(), Quit_Game);
    }

    // Update is called once per frame
    void Update()
    {
        //line.material.SetTextureOffset("_MainTex", Vector2.left * Time.time);
        if (!levelManager.isDoneMachineTUT)
        {
            CheckTUTUnlockMachine();
        }
        else if (!levelManager.isDoneClosetTUT)
        {
            CheckTUTCloset();
        }
        else if (!levelManager.isDoneBagClosetTUT)
        {
            CheckTUTBagCloset();
        }
        else
        {
            fxTUT.SetActive(false);
            line.gameObject.SetActive(false);
        }
    }
    public void CheckTUTUnlockMachine()
    {
        if (levelManager.habitatManager.allActiveHabitats.Count <= 0)
        {
            fxTUT.transform.position = Vector3.right * habitat.myTransform.position.x + Vector3.up * 8f + Vector3.forward * habitat.myTransform.position.z;
            player.OpenPanelTUT(0);
            fxTUT.SetActive(true);
            line.SetPosition(0, player.myTransform.position);
            line.SetPosition(1, Vector3.right * habitat.myTransform.position.x + Vector3.up * 0 + Vector3.forward * habitat.myTransform.position.z);
        }
        else if (levelManager.machineManager.allActiveClothMachine.Count <= 0)
        {
            player.OpenPanelTUT(1);
            fxTUT.transform.position = Vector3.right * clothMachine.myTransform.position.x + Vector3.up * 8f + Vector3.forward * clothMachine.myTransform.position.z;
            fxTUT.SetActive(true);
            line.SetPosition(0, player.myTransform.position);
            line.SetPosition(1, Vector3.right * clothMachine.myTransform.position.x + Vector3.up * 0 + Vector3.forward * clothMachine.myTransform.position.z);
        }
        else if (levelManager.closetManager.listClosets.Count <= 0)
        {
            player.OpenPanelTUT(2);
            fxTUT.transform.position = Vector3.right * closet.myTransform.position.x + Vector3.up * 8f + Vector3.forward * closet.myTransform.position.z;
            fxTUT.SetActive(true);
            line.SetPosition(0, player.myTransform.position);
            line.SetPosition(1, Vector3.right * closet.myTransform.position.x + Vector3.up * 0 + Vector3.forward * closet.myTransform.position.z);
        }
        else if (levelManager.checkOutManager.listCheckout.Count <= 0)
        {
            player.OpenPanelTUT(3);
            fxTUT.transform.position = Vector3.right * checkOut.myTransform.position.x + Vector3.up * 8f + Vector3.forward * checkOut.myTransform.position.z;
            fxTUT.SetActive(true);
            line.SetPosition(0, player.myTransform.position);
            line.SetPosition(1, Vector3.right * checkOut.myTransform.position.x + Vector3.up * 0 + Vector3.forward * checkOut.myTransform.position.z);
        }
        else
        {
            bagCloset.checkUnlock.gameObject.SetActive(false);
            //line.gameObject.SetActive(false);
            habitat.checkCollect.gameObject.SetActive(true);
            fxTUT.SetActive(false);
            player.tutPanel.SetActive(false);
            levelManager.Set_isDoneMachineTUT(true);
        }
    }
    public void CheckTUTCloset()
    {
        if (levelManager.checkOutManager.listGrCusCheckout.Count <= 0)
        {
            if (levelManager.customerManager.listGroupsHaveOutfit.Count <= 0)
            {
                if (player.chickenCloths.Count < 1 && clothMachine.checkCollectCloth.gameObject.activeSelf)
                {
                    if (clothMachine.outCloths.Count < 1)
                    {
                        if (clothMachine.ingredients.Count < 1)
                        {
                            if (player.objHave < 1)
                            {
                                player.OpenPanelTUT(4);
                                line.SetPosition(0, player.myTransform.position);
                                line.SetPosition(1, Vector3.right * habitat.myTransform.position.x + Vector3.up * 0 + Vector3.forward * habitat.myTransform.position.z);
                            }
                            else
                            {
                                player.OpenPanelTUT(1);
                                line.SetPosition(0, player.myTransform.position);
                                line.SetPosition(1, Vector3.right * clothMachine.inIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * clothMachine.inIngredientPos.transform.position.z);
                                fxTUT.transform.position = Vector3.right * clothMachine.inIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * clothMachine.inIngredientPos.transform.position.z;
                                fxTUT.SetActive(true);
                            }
                        }
                        else
                        {
                            habitat.checkCollect.gameObject.SetActive(false);
                            line.SetPosition(0, player.myTransform.position);
                            line.SetPosition(1, Vector3.right * clothMachine.inIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * clothMachine.inIngredientPos.position.z);
                            fxTUT.transform.position = Vector3.right * clothMachine.inIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * clothMachine.inIngredientPos.transform.position.z;
                            player.OpenPanelTUT(1);
                            fxTUT.SetActive(true);
                        }
                    }
                    else
                    {
                        line.SetPosition(0, player.myTransform.position);
                        line.SetPosition(1, Vector3.right * clothMachine.outIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * clothMachine.outIngredientPos.position.z);
                        fxTUT.transform.position = Vector3.right * clothMachine.outIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * clothMachine.outIngredientPos.position.z;
                        player.OpenPanelTUT(5);
                        fxTUT.SetActive(true);
                    }
                }
                else
                {
                    player.OpenPanelTUT(2);
                    fxTUT.SetActive(false);
                    clothMachine.checkPush.gameObject.SetActive(false);
                    clothMachine.checkCollectCloth.gameObject.SetActive(false);
                    line.SetPosition(0, player.myTransform.position);
                    line.SetPosition(1, Vector3.right * closet.myTransform.position.x + Vector3.up * 0 + Vector3.forward * closet.myTransform.position.z);
                }
            }
            else
            {
                player.tutPanel.SetActive(false);
                fxTUT.SetActive(false);
                line.gameObject.SetActive(false);
            }
        }
        else
        {
            if (player.coinValue <= 100)
            {
                if (checkOut.coins.Count <= 0)
                {
                    player.OpenPanelTUT(3);
                    line.gameObject.SetActive(true);
                    line.SetPosition(0, player.myTransform.position);
                    line.SetPosition(1, Vector3.right * checkOut.fxPos.transform.position.x + Vector3.up * 0 + Vector3.forward * checkOut.fxPos.transform.position.z);
                }
                else
                {
                    player.OpenPanelTUT(6);
                    line.SetPosition(0, player.myTransform.position);
                    line.SetPosition(1, Vector3.right * checkOut.coins[0].transform.position.x + Vector3.up * 0 + Vector3.forward * checkOut.coins[0].transform.position.z);
                    fxTUT.transform.position = Vector3.right * checkOut.coins[0].transform.position.x + Vector3.up * 8 + Vector3.forward * checkOut.coins[0].transform.position.z;
                    fxTUT.SetActive(true);
                }
            }
            else
            {
                bagCloset.checkUnlock.gameObject.SetActive(true);
                line.gameObject.SetActive(false);
                fxTUT.SetActive(false);
                player.tutPanel.SetActive(false);
                levelManager.Set_isDoneClosetTUT(true);
            }
        }
    }
    public void CheckTUTBagCloset()
    {
        if (bagCloset.listBags.Count <= 0)
        {
            if(levelManager.closetManager.listBagClosets.Count <= 0)
            {
                player.OpenPanelTUT(8);
                fxTUT.transform.position = Vector3.right * bagCloset.myTransform.position.x + Vector3.up * 8 + Vector3.forward * bagCloset.myTransform.position.z;
                fxTUT.SetActive(true);
                line.gameObject.SetActive(true);
                line.SetPosition(0, player.myTransform.position);
                line.SetPosition(1, Vector3.right * bagCloset.myTransform.position.x + Vector3.up * 0 + Vector3.forward * bagCloset.myTransform.position.z);
            }
            else if (levelManager.machineManager.allActiveBagMachine.Count <= 0)
            {
                player.OpenPanelTUT(7);
                fxTUT.transform.position = Vector3.right * bagMachine.myTransform.position.x + Vector3.up * 8 + Vector3.forward * bagMachine.myTransform.position.z;
                fxTUT.SetActive(true);
                line.SetPosition(0, player.myTransform.position);
                line.SetPosition(1, Vector3.right * bagMachine.myTransform.position.x + Vector3.up * 0 + Vector3.forward * bagMachine.myTransform.position.z);
            }
            else if (player.chickenBags.Count < 1)
            {
                //fxTUT.SetActive(false);
                if (bagMachine.outCloths.Count < 1)
                {
                    if (bagMachine.ingredients.Count < 1)
                    {
                        if (player.objHave < 1)
                        {
                            player.OpenPanelTUT(4);
                            fxTUT.SetActive(false);
                            habitat.checkCollect.gameObject.SetActive(true);
                            line.SetPosition(0, player.myTransform.position);
                            line.SetPosition(1, Vector3.right * habitat.myTransform.position.x + Vector3.up * 0 + Vector3.forward * habitat.myTransform.position.z);
                        }
                        else
                        {
                            player.OpenPanelTUT(7);
                            line.SetPosition(0, player.myTransform.position);
                            line.SetPosition(1, Vector3.right * bagMachine.inIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * bagMachine.inIngredientPos.position.z);
                            fxTUT.transform.position = Vector3.right * bagMachine.inIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * bagMachine.inIngredientPos.position.z;
                            fxTUT.SetActive(true);
                        }
                    }
                    else
                    {
                        player.OpenPanelTUT(7);
                        habitat.checkCollect.gameObject.SetActive(false);
                        line.SetPosition(0, player.myTransform.position);
                        line.SetPosition(1, Vector3.right * bagMachine.inIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * bagMachine.inIngredientPos.position.z );
                        fxTUT.transform.position = Vector3.right * bagMachine.inIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * bagMachine.inIngredientPos.position.z;
                        fxTUT.SetActive(true);
                    }
                }
                else
                {
                    player.OpenPanelTUT(9);
                    line.SetPosition(0, player.myTransform.position);
                    line.SetPosition(1, Vector3.right * bagMachine.outIngredientPos.position.x + Vector3.up * 0 + Vector3.forward * bagMachine.outIngredientPos.position.z);
                    fxTUT.transform.position = Vector3.right * bagMachine.outIngredientPos.position.x + Vector3.up * 8 + Vector3.forward * bagMachine.outIngredientPos.position.z;
                    fxTUT.SetActive(true);
                }
            }
            else
            {
                player.OpenPanelTUT(8);
                fxTUT.SetActive(false);
                line.gameObject.SetActive(true);
                line.SetPosition(0, player.myTransform.position);
                line.SetPosition(1, Vector3.right * bagCloset.myTransform.position.x + Vector3.up * 0 + Vector3.forward * bagCloset.myTransform.position.z);
            }
        }
        else
        {
            levelManager.Set_isDoneBagClosetTUT(true);
            clothMachine.checkPush.gameObject.SetActive(true);
            clothMachine.checkCollectCloth.gameObject.SetActive(true);
            habitat.checkCollect.gameObject.SetActive(true);
            fxTUT.SetActive(false);
            player.tutPanel.SetActive(false);
            line.gameObject.SetActive(false);
        }
    }
    public void DoneAllTuT()
    {
        EnventManager.TriggerEvent(EventName.DoneAllTuT.ToString());
        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("checkpoint_02", "tutorial_end", 1);
        SDK.AdsManager.Instance._ShowBannerAds();
        AppOpenAdManager.Instance.ActiveAoANotShowFistOneShot();
        Dictionary<string, string> pairs_ = new Dictionary<string, string>();
        pairs_.Add("af_success", "true");
        pairs_.Add("af_tutorial_id", DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap.ToString());
        SDK.ABIAppsflyerManager.SendEvent("af_tutorial_completion", pairs_);
        //player.coinValue += 20000;
        //nho xoa khi build 
        if (isBuildTest)
        {
            DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
       DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + 999000);
        }
    
    }
    public bool IsDoneAllTuT()
    {
        foreach (NameTuT nameTuT in nameTuTs_Taget)
        {
            switch (nameTuT)
            {
                case NameTuT.MachineTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneMachineTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.ClosetTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneClosetTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.BagClosetTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneBagClosetTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.CarTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneCarTUT)
                    {
                        return false;
                    }
                    break;
            }
        }
        return true;
    }
    public bool CheckDoneAllTuT()
    {
        foreach (NameTuT nameTuT in nameTuTs_Taget)
        {
            switch (nameTuT)
            {
                case NameTuT.MachineTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneMachineTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.ClosetTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneClosetTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.BagClosetTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneBagClosetTUT)
                    {
                        return false;
                    }
                    break;
                case NameTuT.CarTUT:
                    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].isDoneCarTUT)
                    {
                        return false;
                    }
                    break;
            }
        }

        DoneAllTuT();
        return true;
    }
    //private void Quit_Game()
    //{
    //    if (!GameManager.Instance.listLevelManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].dataLevelManager.Get_isDoneAllTUT())
    //    {
    //      //  DataManager.Instance.ClearAllData();
    //        EnventManager.TriggerEvent(EventName.ClearData.ToString());
    //    }
       
    //}
    public enum NameTuT
    {
        MachineTUT,
        ClosetTUT,
        BagClosetTUT,
        CarTUT
    }
}
