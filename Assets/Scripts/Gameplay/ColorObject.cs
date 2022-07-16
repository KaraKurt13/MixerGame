using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public Color colorOfObject;

    public delegate void ColorSelect(Color selectedColor);
    public static event ColorSelect colorSelected;

    private void OnMouseDown()
    {
        //Destroy(gameObject);
        PlayAnimation();

    }

    private void PlayAnimation()
    {
        colorSelected(colorOfObject);
    }


}
