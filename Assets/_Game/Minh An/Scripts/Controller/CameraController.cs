using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : GenerticSingleton<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] private CinemachineFramingTransposer cinemachineFramingTransposer;
    public override void Awake()
    {
        base.Awake();
        cinemachineFramingTransposer = cinemachineVirtual.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    public void SetFollowAndLookAt(Transform transformFollow, Transform transformLookAt, 
        bool isResetFollowPlayer = false, float timeDelayFollow = 0, 
        float XDamping = 1, float YDamping = 1, float ZDamping = 1, System.Action actionStartFollow = null, System.Action actionEndFollow = null)
    {
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
            }, 5));
        }
        else
        {
            actionEndFollow?.Invoke();
        }
       
    }
    public void ResetFollowPlayer()
    {
        SetFollowAndLookAt(Player.Instance.myTransform, Player.Instance.myTransform);
       // cinemachineFramingTransposer.m_CameraDistance = 45;
    }
    IEnumerator IE_DelayAction(System.Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }
}
