using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorObject : MonoBehaviour
{
    public Color colorOfObject;

    public delegate void ColorObjectEvent(ColorObject selectedObject);
    public static event ColorObjectEvent objectSelected;
    public static event ColorObjectEvent objectAnimationEnded;
    private bool objectIsSelected;

    private void OnMouseDown()
    {
        if(objectIsSelected)
        {
            return;
        }

        objectSelected(this);
        objectIsSelected = false;

    }

    public void AddObjectToBlender()
    {
        objectIsSelected = true;
        PlayAnimation();
        //this.gameObject.AddComponent<Rigidbody>();
    }

    private void PlayAnimation()
    {
        Sequence objectSequence = DOTween.Sequence();
        this.gameObject.transform.DOLocalJump(new Vector3(0.0989999995f, 0.4235f, 0.0610000007f),0.5f,0,2,false);
    }

    private void OnEnable()
    {

    }

}
