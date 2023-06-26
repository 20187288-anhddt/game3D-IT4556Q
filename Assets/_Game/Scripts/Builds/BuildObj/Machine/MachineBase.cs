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
    public bool isHaveInStaff;
    public bool isHaveOutStaff;
    public Transform inStaffPos;
    public Transform outStaffPos;
    public UI_InfoBuild uI_InfoBuild;

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

}
