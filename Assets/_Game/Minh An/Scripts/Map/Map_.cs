using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_ : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TutManager tutManager;
    [SerializeField] private BuildController buildController;
    [SerializeField] private MapController mapController;
    private void Awake()
    {
        if (!GameManager.Instance.listLevelManagers.Contains(levelManager))
        {
            GameManager.Instance.listLevelManagers.Add(levelManager);
        }
        if (!GameManager.Instance.tutManagers.Contains(tutManager))
        {
            GameManager.Instance.tutManagers.Add(tutManager);
        }
        if (!GameManager.Instance.buildControllers.Contains(buildController))
        {
            GameManager.Instance.buildControllers.Add(buildController);
        }
        if (!GameManager.Instance.mapControllers.Contains(mapController))
        {
            GameManager.Instance.mapControllers.Add(mapController);
        }
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }
    public TutManager GetTutManager()
    {
        return tutManager;
    }
    public BuildController GetBuildController()
    {
        return buildController;
    }
    public void OpenMap()
    {
        gameObject.SetActive(true);
    }
    public void CloseMap()
    {
        gameObject.SetActive(false);
    }
}
