using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraIntroAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 firstStepCameraPosition;
    [SerializeField] private Vector3 firstStepCameraRotation;
    [SerializeField] private float firstAnimationDuration;

    [SerializeField] private Vector3 secondStepCameraPosition;
    [SerializeField] private Vector3 secondStepCameraRotation;
    [SerializeField] private float secondAnimationDuration;

    public delegate void CameraAnimationEvent();
    public static event CameraAnimationEvent introFinished;

    void Start()
    {
        PlayIntroAnimation();
    }

    private void PlayIntroAnimation()
    {
        Sequence cameraSequance = DOTween.Sequence();
        cameraSequance.Append(Camera.main.transform.DOMove(firstStepCameraPosition, firstAnimationDuration, false))
            .Join(Camera.main.transform.DORotate(firstStepCameraRotation, firstAnimationDuration));

        cameraSequance.OnComplete(() => {

        cameraSequance.Append(Camera.main.transform.DOMove(secondStepCameraPosition, secondAnimationDuration))
            .Join(Camera.main.transform.DORotate(secondStepCameraRotation, secondAnimationDuration));

        });

    }
}
