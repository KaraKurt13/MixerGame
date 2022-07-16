using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    public Color requiredColor;
    private List<Color> colorsInBlender;
    private Color resultColor;

    private void AddColorToBlender(Color colorToAdd)
    {
        colorsInBlender.Add(colorToAdd);
    }

    public void ClearBlender()
    {
        colorsInBlender.Clear();
        resultColor = null;
    }

    private void OnEnable()
    {
        ColorObject.colorSelected += AddColorToBlender;
    }
}
