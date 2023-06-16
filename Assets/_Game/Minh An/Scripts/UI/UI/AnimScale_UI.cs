using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScale_UI : MonoBehaviour
{
    public Transform myTransform;
    [SerializeField] private AnimationCurve animationScaleCurveX;
    [SerializeField] private AnimationCurve animationScaleCurveY;
    [SerializeField] private AnimationCurve animationScaleCurveZ;
    [SerializeField] private bool isLoop;
    [SerializeField] private float speed = 1;
    private void OnEnable()
    {
        OnInIt();
    }
    private void OnInIt()
    {
        if(myTransform == null) { myTransform = this.transform; }
        PlayAnim();
    }
    private void PlayAnim()
    {
        StartCoroutine(IE_AnimScale());
    }
    IEnumerator IE_AnimScale()
    {
        float m_time = 0;
        while(m_time < 1)
        {
            myTransform.localScale = Vector3.right * animationScaleCurveX.Evaluate(m_time) + Vector3.up * animationScaleCurveY.Evaluate(m_time) + Vector3.forward * animationScaleCurveZ.Evaluate(m_time);
            m_time += Time.deltaTime * speed;
            yield return null;
        }
        if (isLoop)
        {
            PlayAnim();
        }
    }
}
