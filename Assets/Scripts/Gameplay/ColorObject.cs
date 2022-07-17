using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorObject : MonoBehaviour
{
    public Color colorOfObject;

    public delegate void ColorObjectEvent(ColorObject selectedObject);
    public static event ColorObjectEvent objectSelected;

    public delegate void ColorObjectAnimation();
    public static event ColorObjectAnimation objectAnimationEnded;
    private bool objectIsSelected;

    public void AddObject()
    {
        objectIsSelected = true;
        PlayJumpAnimation();
    }

    public void PlayAppearAnimation()
    {
        Vector3 standartScale = gameObject.transform.localScale;
       this.gameObject.transform.localScale = Vector3.zero;
       this.gameObject.transform.DOScale(standartScale, 1).SetEase(Ease.InOutBounce);
    }

    private void PlayJumpAnimation()
    {
        Sequence objectSequence = DOTween.Sequence();
        this.gameObject.transform.DOLocalJump(new Vector3(0.09229f, 0.20679f, 0.0435f), 0.5f,0,2,false);

        objectSequence.OnComplete(() =>
        {
            objectAnimationEnded();
            this.transform.parent.GetComponent<ObjectPooling>().SpawnObjectFromPool();
        });
    }

    private void OnMouseDown()
    {
        if (objectIsSelected)
        {
            return;
        }

        objectSelected(this);
        objectIsSelected = false;

    }

}
