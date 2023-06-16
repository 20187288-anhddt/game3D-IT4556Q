using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Action\\ApparatusProcess")]
public class ApparatusProcess : ScriptableObject
{
    public MissionProcess missionProcess;
    public RewardProcessCompleteMission rewardProcessCompleteMission;
}

