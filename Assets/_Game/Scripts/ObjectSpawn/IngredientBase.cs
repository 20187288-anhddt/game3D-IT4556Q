using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientBase : AllPool
{
    public static float yOffset = 0;
    public IngredientType ingredientType;
    public float ingreScale;
    public void MoveToICollect(BaseActor baseActor)
    {
        float x = Random.Range(-0.05f, 0.05f);
        float z = Random.Range(-0.05f, 0.05f);
        (baseActor as ICollect).AddIngredient(this);
        baseActor.ObjHave++;
        this.transform.parent = baseActor.CarryPos;
        transform.localRotation = Quaternion.identity;
        yOffset += ingreScale;
        //transform.DOMove(baseActor.CarryPos.position, 0.1f).OnComplete(() =>
        //{
        this.transform.DOLocalJump(Vector3.up * yOffset + Vector3.right * x + Vector3.forward * z, 0.75f, 1, 0.25f).OnComplete(() =>
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
    public float GetYOffset()
    {
        return yOffset;
    }
    public void ReSetYOffset()
    {
        yOffset = 0;
    }
    public void AddYOffset(float value)
    {
        yOffset += value;
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
    BEAR_BAG
}
