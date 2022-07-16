using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FridgeIntroAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeAnim;
    [SerializeField] private float animDuration;

    public delegate void FridgeAnim();
    public static event FridgeAnim fridgeAnimationEnded;

    private void Start()
    {
        PlayAnimation();
    }


    private void PlayAnimation()
    {
        Sequence fridgeSequence = DOTween.Sequence();

        fridgeSequence.AppendInterval(delayBeforeAnim);

        fridgeSequence.Append(this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), animDuration));

        fridgeSequence.OnComplete(()=>
        {
            fridgeAnimationEnded();
        });
    }
}
