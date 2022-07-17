using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlenderAnimationController : MonoBehaviour
{
    [SerializeField] private Transform blenderTransform;
    [SerializeField] private Transform liquidTransform;
    [SerializeField] private Transform lidTransform;
    [SerializeField] private Vector3 liquidFinallPosition;
    [SerializeField] private Vector3 lidOpenedPosition;
    [SerializeField] private Vector3 lidOpenedRotation;
    [SerializeField] private AudioClip blenderSound;
    private Vector3 lidClosedRotation;
    private Vector3 lidClosedPosition;
    
    public void PlayShakingAnimation()
    {
        Sequence blenderSequence = DOTween.Sequence();

        blenderSequence.AppendInterval(2);
        blenderSequence.Append(blenderTransform.DOShakePosition(4, 0.005f, 4, 2, false, true));
    }

    public void PlayBlendingAnimation()
    {
        Sequence blenderSequence = DOTween.Sequence();
        PlayLiquidAnimation();
        GetComponent<AudioSource>().PlayOneShot(blenderSound);
        blenderSequence.Append(blenderTransform.DOShakePosition(5, 0.01f, 15, 4, false, true));
    }

    public void PlayObjectBlendingAnim(List<Transform> objects)
    {
        foreach(Transform colorObject in objects)
        {
            colorObject.DOScale(0f, 2f).SetEase(Ease.InExpo);
        }
    }

    public void PlayLiquidAnimation()
    {
        liquidTransform.DOLocalMove(liquidFinallPosition, 4, false);
    }

    public void PlayLidOpeningAnim()
    {
        Sequence lidSequence = DOTween.Sequence();
        lidSequence.Append(lidTransform.DOLocalMove(lidOpenedPosition, 2, false)).Join(
            lidTransform.DOLocalRotate(lidOpenedRotation,2));
    }

    public void PlayLidClosingAnim()
    {
        Sequence lidSequence = DOTween.Sequence();
        lidSequence.Append(lidTransform.DOLocalMove(lidClosedPosition, 2, false)).Join(
            lidTransform.DOLocalRotate(lidClosedRotation, 2));
    }

    private void Start()
    {
        lidClosedPosition = lidTransform.localPosition;
        lidClosedRotation = lidTransform.localRotation.eulerAngles;
    
    }

    private void OnEnable()
    {
        ColorObject.objectAnimationEnded += PlayShakingAnimation;
    }

    private void OnDisable()
    {
        ColorObject.objectAnimationEnded -= PlayShakingAnimation;
    }


}
