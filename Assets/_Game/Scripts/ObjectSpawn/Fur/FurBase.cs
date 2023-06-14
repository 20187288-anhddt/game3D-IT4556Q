using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Utilities.Components;
using MoreMountains.NiceVibrations;

public class FurBase :  IngredientBase
{
    public void MoveToMachine(MachineBase machine, ICollect actor)
    {
        transform.parent = machine.inIngredientPos;
        transform.localRotation = Quaternion.identity;
        if (!machine.ingredients.Contains(this))
        {
            machine.ingredients.Add(this);
            machine.numInputSave++;
        }    
        this.transform.DOLocalJump(Vector3.up * machine.ingredients.Count * ingreScale
            + Vector3.right * 0
            + Vector3.forward * 0, 3f, 1, 0.5f).OnComplete(() =>
        {
            machine.ShortCutIngredients();
            if (actor is Player)
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[5], 1, false);
            }
            //baseActor.ShortObj();
        }).SetEase(Ease.OutCirc);
        //this.transform.DOMoveY(this.transform.position.y + 0.5f, 0f).OnComplete(() =>
        //{
        //    transform.DOLocalJump(Vector3.up * machine.ingredients.Count * ingreScale
        //        + Vector3.right * this.transform.position.x
        //        + Vector3.forward * this.transform.position.z, 5f, 1, 0.5f/*+ build.posPizzaNormal.position*/, 
        //        5f,1,0.5f).OnComplete(() =>
        //    {
        //    machine.ShortCutIngredients();
        //        //if (isPlayer)
        //        //{
        //        //    Vibration.Vibrate(25);
        //        //    AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[2], 1, false);
        //        //}
        //    }).SetEase(Ease.OutCirc);
        //}).SetEase(Ease.OutCirc);
    }
}
