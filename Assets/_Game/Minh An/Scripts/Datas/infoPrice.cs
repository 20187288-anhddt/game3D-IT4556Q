using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Player")]
public class infoPrice : ScriptableObject
{
    public int Price;
    public List<InfoBuy> infoBuys;
}
