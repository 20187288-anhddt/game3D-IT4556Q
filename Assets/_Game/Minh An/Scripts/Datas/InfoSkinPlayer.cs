using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InfoSkinPlayer")]
public class InfoSkinPlayer : ScriptableObject
{
    public int ID;
    public Object modelSkin;
    public Sprite Icon;
    public InfoBuy infoBuy;
}
