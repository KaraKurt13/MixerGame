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

    public delegate void BlenderStatus();
    public static event BlenderStatus blenderIsAvaible;

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
        float colorSimilarity = Vector3.Distance(new Vector3(requiredColor.r, requiredColor.g, requiredColor.b), new Vector3(resultColor.r, resultColor.g, resultColor.b));
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
