using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Player")]
public class infoHire : ScriptableObject
{
    public int Hire;
    public List<InfoBuy> infoBuys;
}
