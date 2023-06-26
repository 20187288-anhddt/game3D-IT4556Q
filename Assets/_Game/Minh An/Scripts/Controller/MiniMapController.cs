using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public TypeLevel typeLevelThis;
    [SerializeField] private Transform pointSpawnPlayer;

    [SerializeField] private Transform pointFollow_Farm;
    [SerializeField] private Transform pointFollow_Machine;
    [SerializeField] private Transform pointFollow_Shop;

    public Vector3 GetPoint_SpwanPlayer()
    {
        return pointSpawnPlayer.position;
    }
    public Transform GetPointFollow_Farm()
    {
        return pointFollow_Farm;
    }
    public Transform GetPointFollow_Machine()
    {
        return pointFollow_Machine;
    }
    public Transform GetPointFollow_Shop()
    {
        return pointFollow_Shop;
    }
    public void OpenMiniMap()
    {
        gameObject.SetActive(true);
    }
    public void CloseMiniMap()
    {
        gameObject.SetActive(false);
    }
    public enum TypeLevel
    {
        Level_1 = 1,
        Level_2 = 2,
        Level_3 = 3
    }
}
