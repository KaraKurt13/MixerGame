using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blender : MonoBehaviour
{
    
    [SerializeField] private List<Color> colorsInBlender;
    [SerializeField] private List<Transform> colorsObjects;
    [SerializeField] private GameObject resultLiquid;
    [SerializeField] private Color resultColor;
    public Color requiredColor;
    private bool blendingIsInProgress;
    private bool lidIsOpened;
    private IEnumerator lidOpeningCoroutine;

    private BlenderAnimationController blenderAnim;

    public delegate void BlenderResults(int result);
    public static event BlenderResults colorIsSimilarToRequired;
    public static event BlenderResults colorIsNotSimilarToRequired;

    private void AddObjectToBlender(ColorObject objectToAdd)
    {
        if(blendingIsInProgress)
        {
            return;
        }

        if (!lidIsOpened)
        {
            lidIsOpened = true;
            blenderAnim.PlayLidOpeningAnim();
            StartCoroutine(lidOpeningCoroutine);
        }
        else
        {
            StopCoroutine(lidOpeningCoroutine);
            lidIsOpened = true;
            lidOpeningCoroutine = LidState();
            StartCoroutine(lidOpeningCoroutine);
        } 

        objectToAdd.AddObject();
        colorsInBlender.Add(objectToAdd.colorOfObject);
        colorsObjects.Add(objectToAdd.gameObject.transform);
        
    }

    private IEnumerator LidState()
    {
        yield return new WaitForSeconds(4f);

        blenderAnim.PlayLidClosingAnim();
        lidIsOpened = false;
    }

    private IEnumerator BlendColors()
    {
        StopCoroutine(lidOpeningCoroutine);
        
        blenderAnim.PlayLidClosingAnim();
        lidIsOpened = false;

        resultColor = new Color(0,0,0,0);

        foreach(Color col in colorsInBlender)
        {
            resultColor += col;
        }

        resultColor /= colorsInBlender.Count;

        resultLiquid.GetComponent<MeshRenderer>().material.color = resultColor;
        blenderAnim.PlayBlendingAnimation();
        blenderAnim.PlayObjectBlendingAnim(colorsObjects);

        yield return new WaitForSeconds(3);
        
        CheckColorForRequired();
    }

    private void CheckColorForRequired()
    {
        float colorSimilarity = Mathf.Clamp(100 - (Mathf.Sqrt(Mathf.Pow(requiredColor.r - resultColor.r, 2) + Mathf.Pow(requiredColor.g - resultColor.g, 2) + Mathf.Pow(requiredColor.b - resultColor.b, 2))*100),0,100);

        if (colorSimilarity>90)
        {
            colorIsSimilarToRequired((int)colorSimilarity);
        }
        else
        {
            colorIsNotSimilarToRequired((int)colorSimilarity);
        }

    }

    private void OnEnable()
    {
        ColorObject.objectSelected += AddObjectToBlender;
    }

    private void OnDisable()
    {
        ColorObject.objectSelected -= AddObjectToBlender;
    }

    private void OnMouseDown()
    {
        if(blendingIsInProgress || colorsInBlender.Count==0)
        {
            return;
        }

        blendingIsInProgress = true;
        StartCoroutine(BlendColors());
    }

    private void Start()
    {
        colorsInBlender = new List<Color>();
        blenderAnim = GetComponent<BlenderAnimationController>();
        blendingIsInProgress = false;
        lidIsOpened = false;
        lidOpeningCoroutine = LidState();
    }
}
