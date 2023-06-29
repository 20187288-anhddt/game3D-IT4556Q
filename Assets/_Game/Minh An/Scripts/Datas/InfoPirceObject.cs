using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Object\\Pirce")]
public class InfoPirceObject : ScriptableObject
{
    public string nameString;
    public StatusObject.Level level;
    public List<InfoThis> infoThese;
    public NameObject_This nameObject_This;
    public List<InfoBuy> infoBuys;
}
[System.Serializable]
public class InfoThis
{
    public TypeBuff typeBuff;
    public float value;

    public float GetValue()
    {
        return value;
    }

    public enum TypeBuff
    {
        Speed,
        Stack
    }
}
