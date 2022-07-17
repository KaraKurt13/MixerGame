using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blender : MonoBehaviour
{
    
    [SerializeField] private List<Color> colorsInBlender;
    [SerializeField] private GameObject resultLiquid;
    private Color resultColor;
    public Color requiredColor;
    private bool blendingIsInProgress;
    private bool lidIsOpened;
    private bool blendingIsPossible;
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

        StartCoroutine(ObjectJumpDelay());

        if (!lidIsOpened)
        {
            lidIsOpened = true;
            blenderAnim.PlayLidOpeningAnim();
            Debug.Log("Should start!");
            StartCoroutine(lidOpeningCoroutine);
        }
        else
        {
            Debug.Log("Should restart!");
            StopCoroutine(lidOpeningCoroutine);
            lidIsOpened = true;
            lidOpeningCoroutine = LidState();
            StartCoroutine(lidOpeningCoroutine);
        } 

        objectToAdd.AddObject();
        colorsInBlender.Add(objectToAdd.colorOfObject);
        
    }

    public void ClearBlender()
    {
        colorsInBlender.Clear();
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

        yield return new WaitForSeconds(3);
        
        CheckColorForRequired();
    }

    private IEnumerator ObjectJumpDelay()
    {
        blendingIsPossible = false;
        yield return new WaitForSeconds(2);
        blendingIsPossible = true;
    }

    private void CheckColorForRequired()
    {
        float colorSimilarity = 100-Mathf.Abs(Vector3.Distance(new Vector3(requiredColor.r, requiredColor.g, requiredColor.b), new Vector3(resultColor.r, resultColor.g, resultColor.b))*100);

        if (colorSimilarity<0.1)
        {
            colorIsSimilarToRequired((int)colorSimilarity);
        }
        else
        {
            colorIsNotSimilarToRequired((int)colorSimilarity);
        }

        Debug.Log(colorSimilarity);
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
        if(blendingIsInProgress || colorsInBlender.Count==0 || blendingIsPossible)
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
        blendingIsPossible = true;
        lidIsOpened = false;
        lidOpeningCoroutine = LidState();
    }
}
