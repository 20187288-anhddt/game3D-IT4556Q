using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextLevel : MonoBehaviour
{
    [SerializeField] private NextLevel nextLevel;
    private void OnTriggerEnter(Collider other)
    {
        nextLevel.NextLevel_();
    }
}
