using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using Utilities.Components;

public class Coin : AllPool
{
    [SerializeField]
    private float moveSpeed;
    //public float moveSpeed;
    //protected void Start()
    //{
    //    base.Start();
    //}

    //protected void Update()
    //{
    //    base.Update();
    //    //transform.RotateAround(transform.position, Vector3.up, rotSpeed * Time.deltaTime);
    //}

    public void MoveToPlayerSpeed(Player player)
    {
        float distance = Vector3.Distance(player.transform.position + Vector3.up * 0.5f, this.transform.position);
        float time = (float)distance / moveSpeed;
        transform.rotation = Quaternion.identity;
        transform.DOJump(player.myTransform.position+ Vector3.up*1.5f, 5f, 1, 0.15f).OnComplete(() => {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            AllPoolContainer.Instance.Release(this);
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[15], 1, false);
            //Vibration.Vibrate(25);
        }).SetEase(Ease.OutCirc);
    }
    //public void MoveToThief(Thief thief)
    //{
    //    float distance = Vector3.Distance(thief.transform.position + Vector3.up * 0.5f, this.transform.position);
    //    float time = (float)distance / moveSpeed;
    //    float x = Random.RandomRange(-0.05f, 0.05f);
    //    float z = Random.RandomRange(-0.05f, 0.05f);
    //    this.transform.parent = thief.backPos;
    //    transform.localRotation = Quaternion.identity;
    //    thief.coinList.Add(this);
    //    transform.DOLocalJump(Vector3.up * thief.coinList.Count * 0.03f + Vector3.right * x + Vector3.forward * z, 2.5f, 1, 0.2f).OnComplete(() => {
    //    }).SetEase(Ease.Linear);
    //}
    //public override void OnUsed()
    //{

    //}
    public void MoveToBuildLock(Vector3 trans, float time)
    {
        this.transform.DOJump(trans,1f,1, time).OnComplete(() =>
        {
            AllPoolContainer.Instance.Release(this);
            //Vibration.Vibrate(20);
        }).SetEase(Ease.OutCirc);
    }
    //public void MoveToPlayer(Player player, float time)
    //{
    //    transform.DOMove(player.transform.position + Vector3.up, time).OnComplete(() => {
    //        AllPoolContainer.Instance.Release(this);
    //        Vibration.Vibrate(25);
    //    }).SetEase(Ease.Linear);
    //}
}
