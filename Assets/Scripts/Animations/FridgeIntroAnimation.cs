using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FridgeIntroAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeAnim;
    [SerializeField] private float animDuration;

    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }


    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(delayBeforeAnim);
        Sequence cameraSequance = DOTween.Sequence();
        cameraSequance.Append(this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), animDuration));
    }
}
