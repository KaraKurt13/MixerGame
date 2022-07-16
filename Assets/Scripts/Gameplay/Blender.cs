using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    public Color requiredColor;
    [SerializeField] private List<Color> colorsInBlender;
    [SerializeField] private MeshRenderer resultLiquid;
    private Color resultColor;
    private bool blendingIsInProgress;

    public delegate void BlenderAvaibility();
    public static event BlenderAvaibility blenderIsAvaible;

    public delegate void BlenderResults(int result);
    public static event BlenderResults colorIsSimilarToRequired;
    public static event BlenderResults colorIsNotSimilarToRequired;

    private void Start()
    {
        colorsInBlender = new List<Color>();
        blendingIsInProgress = false;
    }

    private void AddObjectToBlender(ColorObject objectToAdd)
    {
        if(blendingIsInProgress)
        {
            return;
        }

        //blenderIsAvaible();
        objectToAdd.AddObjectToBlender();
        colorsInBlender.Add(objectToAdd.colorOfObject);
        
    }

    public void ClearBlender()
    {
        colorsInBlender.Clear();
        
    }

    private IEnumerator BlendColors()
    {
        resultColor = new Color(0,0,0,0);
        foreach(Color col in colorsInBlender)
        {
            resultColor += col;
        }

        resultColor /= colorsInBlender.Count;

        yield return new WaitForSeconds(5);
        resultLiquid.material.color = resultColor;
        CheckColorForRequired();
        blendingIsInProgress = false;
    }

    private void CheckColorForRequired()
    {
        float colorSimilarity = 100-Mathf.Abs(Vector3.Distance(new Vector3(requiredColor.r, requiredColor.g, requiredColor.b), new Vector3(resultColor.r, resultColor.g, resultColor.b))*100);

        if(colorSimilarity<0.1)
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

    private void OnMouseDown()
    {
        if(blendingIsInProgress)
        {
            return;
        }

        blendingIsInProgress = true;
        StartCoroutine(BlendColors());
    }
}
