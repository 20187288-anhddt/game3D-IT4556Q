using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class IngredientBase : AllPool
{
    public Transform myTransform;
    public IngredientType ingredientType;
    public float ingreScale;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void MoveToICollect(ICollect actor)
    {
        float x = Random.Range(-0.05f, 0.05f);
        float z = Random.Range(-0.05f, 0.05f);
        (actor as ICollect).AddIngredient(this);
        actor.objHave++;
        this.transform.parent = actor.carryPos;
        transform.localRotation = Quaternion.identity;
        // yOffset += ingreScale;
        actor.yOffset += ingreScale;
        //transform.DOMove(baseActor.CarryPos.position, 0.1f).OnComplete(() =>
        //{
        this.transform.DOLocalJump(Vector3.up * actor.yOffset+ Vector3.right * x + Vector3.forward * z, 5f, 1, 0.5f).OnComplete(() =>
        {
            //baseActor.ShortObj();
            if (actor is Player)
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[5], 1, false);
            }

        }).SetEase(Ease.OutCirc);
        //});  
    }
   
    public void MoveToTrash(TrashCan gar,ICollect actor)
    {
        float x = Random.Range(-0.05f, 0.05f);
        float z = Random.Range(-0.05f, 0.05f);
        this.transform.DOMoveY(this.transform.position.y + 2f, 0.25f).OnComplete(() =>
        {
            transform.DOMove(gar.transform.position, 0.25f).OnComplete(() =>
            {
                this.transform.parent = null;
                AllPoolContainer.Instance.Release(this);
                if (actor is Player)
                {
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[5], 1, false);
                }
            }).SetEase(Ease.OutCirc);
        }).SetEase(Ease.OutCirc);
    }
    public void MoveToCar(GameObject car)
    {
        this.transform.DOMoveY(this.transform.position.y + 2f, 0.25f).OnComplete(() =>
        {
            transform.DOMove(car.transform.position, 0.25f).OnComplete(() =>
            {
                this.transform.parent = null;
                AllPoolContainer.Instance.Release(this);
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }).SetEase(Ease.OutCirc);
        }).SetEase(Ease.OutCirc);
        //AllPoolContainer.Instance.Release(this);
    }
}

public enum IngredientType
{
    SHEEP,
    SHEEP_CLOTH,
    COW,
    COW_CLOTH,
    BEAR,
    BEAR_CLOTH,
    CHICKEN,
    CHICKEN_CLOTH,
    NONE,
    SHEEP_BAG,
    COW_BAG,
    CHICKEN_BAG,
    BEAR_BAG,
    CHECKOUT,
    BUILDSTAGE,
    HIRESTAFF,
    CAR,
    SHIT,
    HIRE_ANIMAL,
    LION,
    CROC,
    ELE,
    ZEBRA,
    LION_CLOTH,
    CROC_CLOTH,
    ELE_CLOTH,
    ZEBRA_CLOTH,
    LION_BAG,
    CROC_BAG,
    ELE_BAG,
    ZEBRA_BAG
}
