using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Player")]
public class infoSpeed : ScriptableObject
{
    public float Speed;
    public List<InfoBuy> infoBuys;

}
