using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothMachine : MachineBase
{
    public List<ClothBase> outCloths;
    [SerializeField]
    public ClothBase clothPrefab;
    [SerializeField]
    private GameObject unlockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckPush checkPush;
    [SerializeField]
    private CheckCollectCloth checkCollectCloth;
   
    private void LoadAndSetData()
    {
        maxObjOutput = maxObjInput = (int)(dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Stack().infoThese[0].value;
        timeDelay = (dataStatusObject as MachineDataStatusObject).GetInfoPirceObject_Speed().infoThese[0].value;
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
      
        p.isUnlock = true;
      //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        {
            p.myTransform.position = checkUnlock.myTransform.position - Vector3.left * 7;
        }
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                       // EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        checkCollectCloth.gameObject.SetActive(true);
                        checkPush.gameObject.SetActive(true);
                        uI_InfoBuild.gameObject.SetActive(true);
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
            //EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
            checkCollectCloth.gameObject.SetActive(true);
            checkPush.gameObject.SetActive(true);
            uI_InfoBuild.gameObject.SetActive(true);
        }
        checkUnlock.gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = true;
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                if(!levelManager.machineManager.listBearClothMachineActive.Contains(this))
                    levelManager.machineManager.listBearClothMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.BearClothMachine:
                            EnventManager.TriggerEvent(EventName.BearClothMachine_Complete.ToString());
                            break;
                    }
                }
               
                break;
            case IngredientType.COW:
                if (!levelManager.machineManager.listCowClothMachineActive.Contains(this))
                    levelManager.machineManager.listCowClothMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.CowClothMachine:
                            EnventManager.TriggerEvent(EventName.CowClothMachine_Complete.ToString());
                            break;
                    }
                }
             
                break;
            case IngredientType.CHICKEN:
                if (!levelManager.machineManager.listChickenClothMachineActive.Contains(this))
                    levelManager.machineManager.listChickenClothMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.ChickenClothMachine:
                            EnventManager.TriggerEvent(EventName.ChickenClothMachine_Complete.ToString());
                            break;
                    }
                }
               
                break;
            case IngredientType.SHEEP:
                if (!levelManager.machineManager.listSheepClothMachineActive.Contains(this))
                    levelManager.machineManager.listSheepClothMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.SheepClothMachine:
                            EnventManager.TriggerEvent(EventName.SheepClothMachine_Complete.ToString());
                            break;
                    }
                }
              
                break;
        }
        if (!levelManager.machineManager.allActiveMachine.Contains(this))
            levelManager.machineManager.allActiveMachine.Add(this);
        if (!levelManager.machineManager.allActiveClothMachine.Contains(this))
            levelManager.machineManager.allActiveClothMachine.Add(this);
    }
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    void Update()
    {
        Effect();
    }
    public override void Effect()
    {
        if (!IsLock)
        {
            uI_InfoBuild.Active(true);
            uI_InfoBuild.LoadTextProcess(ingredients.Count.ToString() + "/" + maxObjOutput.ToString());
        }
        else
        {
            uI_InfoBuild.Active(false);
        }
        if (outCloths.Count >= maxObjOutput)
        {
            return;
        }
        if(ingredients.Count > 0)
        {
            if(isReadyInToMid && outCloths.Count < maxObjOutput)
            {
                isReadyInToMid = false;
                CounterHelper.Instance.QueueAction(delayInput, () => 
                {
                    InputMoveToCenter();
                });        
            }        
        }
        if (!isReadyInToMid)
        {
            if (isReadyMidToOut)
            {
                isReadyMidToOut = false;
                OutputMoveToEnd();
            }
        }
    }
    //private void SpawnObject()
    //{
    //    var curCloth = AllPoolContainer.Instance.Spawn(clothPrefab, cenIngredientPos.transform.position, transform.rotation);
    //    curCloth.transform.DOMove(outIngredientPos.position, timeDelay / 2).OnComplete(() =>
    //    {
    //        outCloths.Add(curCloth as ClothBase);
    //        if (outCloths.Count >= maxObjOutput)
    //        {
    //            return;
    //        }
    //    });
    //}

    public void ShortCutOutCloth()
    {
        if (outCloths.Count > 0)
        {
            for (int i = 0; i < outCloths.Count; i++)
            {
                outCloths[i].myTransform.localPosition = new Vector3(outCloths[i].myTransform.localPosition.x,
                    (i) * outCloths[i].ingreScale,
                    outCloths[i].myTransform.localPosition.z);
            }
        }
    }
    private Vector3 GetClothPos(int value)
    {
        if (outCloths.Count <= 0)
        {
            curClothPos = outIngredientPos.position;
        }
        else
        {
            curClothPos = outCloths[0].myTransform.position;
        }
        curClothPos += Vector3.up * value * clothPrefab.ingreScale;
        return curClothPos;
    }
    private void InputMoveToCenter()
    {
        var curIngredient = ingredients[0];
        ingredients.Remove(curIngredient);
        ShortCutIngredients();
        curIngredient.myTransform.DOMove(cenIngredientPos.position, timeDelay/2).OnComplete(() =>
        {
            AllPoolContainer.Instance.Release(curIngredient);
            isReadyMidToOut = true;     
        });
        
    }
    private void OutputMoveToEnd()
    {
        GetClothPos(outCloths.Count);
        var curCloth = AllPoolContainer.Instance.Spawn(clothPrefab, cenIngredientPos.transform.position, myTransform.rotation);
        curCloth.transform.parent = outIngredientPos;
        curCloth.transform.DOMove(outIngredientPos.position, timeDelay / 2).OnComplete(() =>
        {
            outCloths.Add(curCloth as ClothBase);
            curCloth.transform.position = curClothPos;
            if (outCloths.Count >= maxObjOutput)
            {
                
            }
            ShortCutOutCloth();
            isReadyInToMid = true;
            isReadyMidToOut = false;
        });
    }
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
           dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        ingredients = new List<FurBase>();
        outCloths = new List<ClothBase>();
        foreach (IngredientBase i in ingredients)
        {
            i.myTransform.parent = null;
            AllPoolContainer.Instance.Release(i);
        }
        isReadyInToMid = true;
        isReadyMidToOut = false; 
        if (isLock)
        {
            checkCollectCloth.gameObject.SetActive(false);
            checkPush.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            uI_InfoBuild.gameObject.SetActive(false);
        }
        //else
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
        EnventManager.AddListener(EventName.ReLoadDataUpgrade.ToString(), LoadAndSetData);
        LoadAndSetData();
    }
}
