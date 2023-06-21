using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : BuildObj
{
    public List<FurBase> ingredients;
    public IngredientType machineType;
    public int maxObjInput;
    public int maxObjOutput;
    protected bool isReadyInToMid;
    protected bool isReadyMidToOut;
    public Transform inIngredientPos;
    public Transform cenIngredientPos;
    public Transform outIngredientPos;
    public float delayInput;
    protected Vector3 curClothPos;
}
