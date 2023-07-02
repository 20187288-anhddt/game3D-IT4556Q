using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : GenerticSingleton<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] private CinemachineFramingTransposer cinemachineFramingTransposer;
    [SerializeField] private CinemachineFollowZoom CinemachineFollowZoom;
    private bool isCameraOnFollowPlayer;
    public override void Awake()
    {
        base.Awake();
        cinemachineFramingTransposer = cinemachineVirtual.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ReLoadDistanceCamera.ToString(), ReLoadDistanceInMap);
        EnventManager.TriggerEvent(EventName.ReLoadDistanceCamera.ToString());
        isCameraOnFollowPlayer = true;
    }
    private void ReLoadDistanceInMap()
    {
        int levelMap = 0;
        switch (DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent())
        {
            case MiniMapController.TypeLevel.Level_1:
                levelMap = 1;
                break;
            case MiniMapController.TypeLevel.Level_2:
                levelMap = 2;
                break;
            case MiniMapController.TypeLevel.Level_3:
                levelMap = 3;
                break;
        }
        SetMoveDistance(30 + levelMap * 5);
    }
    public void SetFollow_LookAt(Transform transformFollow, Transform transformLookAt, float timeDelayFollow = 0, float XDamping = 1, float YDamping = 1, float ZDamping = 1)
    {
        if(cinemachineFramingTransposer == null)
        {
            cinemachineFramingTransposer = cinemachineVirtual.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        StartCoroutine(IE_DelayAction(() =>
        {
            cinemachineVirtual.Follow = transformFollow;
            cinemachineVirtual.LookAt = transformLookAt;

        }, timeDelayFollow));
        cinemachineFramingTransposer.m_XDamping = XDamping;
        cinemachineFramingTransposer.m_YDamping = YDamping;
        cinemachineFramingTransposer.m_ZDamping = ZDamping;
    }
    public void SetFollowAndLookAt(Transform transformFollow, Transform transformLookAt, 
        bool isResetFollowPlayer = false, float timeDelayFollow = 0, float timeDelayResetFollowPlayer = 2.5f,
        float XDamping = 1, float YDamping = 1, float ZDamping = 1, System.Action actionStartFollow = null,
        System.Action actionEndFollow = null, bool isFollowPlayer = false)
    {
       // Debug.Log(timeDelayResetFollowPlayer);
        isCameraOnFollowPlayer = isFollowPlayer;
        UI_Manager.Instance.CloseAll_UI_In_Stack_Open();
        actionStartFollow?.Invoke();
      //  cinemachineFramingTransposer.m_CameraDistance = Vector3.Distance(cinemachineFramingTransposer.transform.position, transformFollow.position);
        cinemachineFramingTransposer.m_XDamping = XDamping;
        cinemachineFramingTransposer.m_YDamping = YDamping;
        cinemachineFramingTransposer.m_ZDamping = ZDamping;
        StartCoroutine(IE_DelayAction(() => 
        {
            cinemachineVirtual.Follow = transformFollow;
            cinemachineVirtual.LookAt = transformLookAt;

        }, timeDelayFollow));
       
        if (isResetFollowPlayer)
        {
            StartCoroutine(IE_DelayAction(() => 
            {
                ResetFollowPlayer();
                actionEndFollow?.Invoke();
                UI_Manager.Instance.OpenUI(NameUI.Canvas_Static);
                UI_Manager.Instance.OpenUI(NameUI.Canvas_Home);
                UI_Manager.Instance.OpenUI(NameUI.Canvas_Bonus);
            }, timeDelayResetFollowPlayer));
        }
        else
        {
            StartCoroutine(IE_DelayAction(() =>
            {
                actionEndFollow?.Invoke();
                UI_Manager.Instance.OpenUI(NameUI.Canvas_Static);
                UI_Manager.Instance.OpenUI(NameUI.Canvas_Home);
            }, timeDelayResetFollowPlayer));

        }
      
    }
    public void MoveDistance(float value, float speedMove)
    {
        StartCoroutine(IE_MoveDistance(value, speedMove));
    }
    public void SetMoveDistance(float value)
    {
        CinemachineFollowZoom.m_Width = value;
    }
    IEnumerator IE_MoveDistance(float value, float speedMove)
    {
        float m_time = 0;
        float StartDistance = CinemachineFollowZoom.m_Width;
        while (m_time < 1)
        {
            CinemachineFollowZoom.m_Width = Mathf.Lerp(StartDistance, value, m_time);
            m_time += Time.deltaTime * speedMove;
            yield return null;
        }
    }
    public void ResetFollowPlayer()
    {
        if(Player.Instance == null)
        {
            return;
        }
        SetFollow_LookAt(Player.Instance.myTransform, Player.Instance.myTransform, 0, 1, 1, 1);
       // cinemachineFramingTransposer.m_CameraDistance = 45;
    }
    IEnumerator IE_DelayAction(System.Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }
    public bool IsCameraFollowPlayer()
    {
        return isCameraOnFollowPlayer;
    }
}
