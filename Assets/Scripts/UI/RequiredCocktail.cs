using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredCocktail : MonoBehaviour
{
    public void SetCocktailImage(Sprite cocktailSprite)
    {
        GetComponent<Image>().sprite = cocktailSprite;
    }
}
