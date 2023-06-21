using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientBase : AllPool
{
    public IngredientType ingredientType;
    public float ingreScale;
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
        this.transform.DOLocalJump(Vector3.up * actor.yOffset + Vector3.right * x + Vector3.forward * z, 0.75f, 1, 0.25f).OnComplete(() =>
        {
            //baseActor.ShortObj();
        });
        //});

        //if (baseActor is Player)
        //{
        //    Vibration.Vibrate(25);
        //    AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[5], 1, false);
        //}
    }
   
    public void MoveToTrash(TrashCan gar)
    {
        float x = Random.Range(-0.05f, 0.05f);
        float z = Random.Range(-0.05f, 0.05f);
        this.transform.DOMoveY(this.transform.position.y + 0.5f, 0.125f).OnComplete(() =>
        {
            transform.DOMove(gar.transform.position, 0.125f).OnComplete(() =>
            {
                this.transform.parent = null;
                AllPoolContainer.Instance.Release(this);
                //if (isPlayer)
                //{
                //    Vibration.Vibrate(25);
                //    AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[2], 1, false);
                //}
            });
        }).SetEase(Ease.Linear);
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
<<<<<<< HEAD
    BEAR_BAG,
<<<<<<< Updated upstream
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
    ZEBRA_BAG,
    NEXTLEVEL
=======
    CHECKOUT
>>>>>>> Stashed changes
=======
    BEAR_BAG
>>>>>>> main
}
