using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BagMachine : MachineBase
{
    public List<BagBase> outCloths;
    [SerializeField]
    private BagBase clothPrefab;
    [SerializeField]
    private GameObject unlockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckPush checkPushBagMachine;
    [SerializeField]
    private CheckCollectBagCloth checkCollectBagCloth;

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
            p.myTransform.position = checkUnlock.myTransform.position + Vector3.left * 6;
        }
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                     //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        checkCollectBagCloth.gameObject.SetActive(true);
                        checkPushBagMachine.gameObject.SetActive(true);
                        uI_InfoBuild.gameObject.SetActive(true);
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
           // EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
            checkCollectBagCloth.gameObject.SetActive(true);
            checkPushBagMachine.gameObject.SetActive(true);
            uI_InfoBuild.gameObject.SetActive(true);
        }
        checkUnlock.gameObject.SetActive(false);   
        //GetComponent<BoxCollider>().enabled = true;
        if (!levelManager.machineManager.allActiveMachine.Contains(this))
            levelManager.machineManager.allActiveMachine.Add(this);
        if (!levelManager.machineManager.allActiveBagMachine.Contains(this))
            levelManager.machineManager.allActiveBagMachine.Add(this);
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                if (!levelManager.machineManager.listBearBagMachineActive.Contains(this))
                    levelManager.machineManager.listBearBagMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.BearBagMachine:
                            EnventManager.TriggerEvent(EventName.BearBagMachine_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.COW:
                if (!levelManager.machineManager.listCowBagMachineActive.Contains(this))
                    levelManager.machineManager.listCowBagMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.CowBagMachine:
                            EnventManager.TriggerEvent(EventName.CowBagMachine_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.CHICKEN:
                if (!levelManager.machineManager.listChickenBagMachineActive.Contains(this))
                    levelManager.machineManager.listChickenBagMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.ChickenBagMachine:
                            EnventManager.TriggerEvent(EventName.ChickenBagMachine_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.SHEEP:
                if (!levelManager.machineManager.listSheepBagMachineActive.Contains(this))
                    levelManager.machineManager.listSheepBagMachineActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.SheepBagMachine:
                            EnventManager.TriggerEvent(EventName.SheepBagMachine_Complete.ToString());
                            break;
                    }
                }
                break;
        }
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
        if (outCloths.Count >= maxObjOutput)
        {
            return;
        }
        if (ingredients.Count > 0)
        {
            if (isReadyInToMid && outCloths.Count < maxObjOutput)
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
        if (!IsLock)
        {
            uI_InfoBuild.Active(true);
            uI_InfoBuild.LoadTextProcess(ingredients.Count.ToString() + "/" + maxObjOutput.ToString());
        }
        else
        {
            uI_InfoBuild.Active(false);
        }
    }
    //private void SpawnObject()
    //{
    //    var curCloth = AllPoolContainer.Instance.Spawn(clothPrefab, cenIngredientPos.transform.position, transform.rotation);
    //    curCloth.transform.DOMove(outIngredientPos.position, timeDelay / 2).OnComplete(() =>
    //    {
    //        outCloths.Add(curCloth as ClothBase);
    //        if(outCloths.Count >= maxObjOutput)
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
        curIngredient.myTransform.DOMove(cenIngredientPos.position, timeDelay / 2).OnComplete(() =>
        {
            AllPoolContainer.Instance.Release(curIngredient);
            isReadyMidToOut = true;
        });

    }
    private void OutputMoveToEnd()
    {
        GetClothPos(outCloths.Count);
        var curCloth = AllPoolContainer.Instance.Spawn(clothPrefab, cenIngredientPos.position, myTransform.rotation);
        curCloth.transform.parent = outIngredientPos;
        curCloth.transform.DOMove(outIngredientPos.position, timeDelay / 2).OnComplete(() =>
        {
            outCloths.Add(curCloth as BagBase);
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
        isHaveInStaff = false;
        isHaveOutStaff = false;
        ingredients = new List<FurBase>();
        outCloths = new List<BagBase>();
        foreach (IngredientBase i in ingredients)
        {
            i.myTransform.parent = null;
            AllPoolContainer.Instance.Release(i);
        }
        isReadyInToMid = true;
        isReadyMidToOut = false;
        if (isLock)
        {
            checkCollectBagCloth.gameObject.SetActive(false);
            checkPushBagMachine.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            uI_InfoBuild.gameObject.SetActive(false);
        }
        else
        {
            UnLock();
        }
        checkUnlock.UpdateUI();
    }
}
