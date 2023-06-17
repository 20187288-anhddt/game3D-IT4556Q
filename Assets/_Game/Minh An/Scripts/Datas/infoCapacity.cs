using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Player")]
public class infoCapacity : ScriptableObject
{
    public int Capacity;
    public List<InfoBuy> infoBuys;
    public Sprite IconThis;
}