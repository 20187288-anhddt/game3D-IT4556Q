using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data\\Action\\EventInMapProcess")]
public class EventInMapProcess : ScriptableObject
{
    public List<EventInMap> eventInMaps;
}
[System.Serializable]
public class EventInMap
{
    public MissionProcess missionProcess;
    public RewardProcessCompleteMission rewardProcessCompleteMission;
}
