using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : BuildObj, ILock
{
    public List<FurBase> ingredients;
    public int maxObjInput;
    public int maxObjOutput;
    protected bool isReadyInToMid;
    protected bool isReadyMidToOut;
    public Transform inIngredientPos;
    public Transform cenIngredientPos;
    public Transform outIngredientPos;
    public float delayInput;
    protected Vector3 curClothPos;
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public int numInputSave;
    public int numOutputSave;
    public bool isHaveInStaff;
    public bool isHaveOutStaff;
    public Transform inStaffPos;
    public Transform outStaffPos;
    public UI_InfoBuild uI_InfoBuild;
    public FurBase furPrefabs;
    public void AddEvent()
    {
        EnventManager.AddListener(EventName.Machine_Double_Speed_Play.ToString(), DoubleSpeed);
        EnventManager.AddListener(EventName.Machine_Double_Speed_Stop.ToString(), ResetSpeed);
    }
    public void DoubleSpeed()
    {
        timeDelay = timeDelay / 2;
    }
    public void ResetSpeed()
    {
        timeDelay *= 2;
    }
    public void ShortCutIngredients()
    {
        if (ingredients.Count > 0)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].transform.localPosition = new Vector3(ingredients[i].transform.localPosition.x,
                    (i) * ingredients[i].ingreScale,
                    ingredients[i].transform.localPosition.z);
            }
        }
    }
    public void SpawnOnStart(int numInput, int numOutput)
    {
        SpawnInputOnStart(numInput);
        SpawnOutputOnStart(numOutput);
    }
    public void SpawnInputOnStart(int n)
    {
        if (n <= 0)
        {
            return;
        }
        else
        {
            for (int i = 1; i <= n; i++)
            {
                var curIngre = AllPoolContainer.Instance.Spawn(furPrefabs, this.inIngredientPos.position, Quaternion.identity);
                curIngre.transform.parent = inIngredientPos;
                curIngre.transform.localRotation = Quaternion.identity;
                if (!ingredients.Contains(curIngre as FurBase))
                    ingredients.Add(curIngre as FurBase);
                curIngre.transform.position = Vector3.up * this.ingredients.Count * (curIngre as FurBase).ingreScale + inIngredientPos.position;
                ShortCutIngredients();
            }
        }
    }
    public virtual void SpawnOutputOnStart(int m)
    {

    }
}
