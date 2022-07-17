using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeItems : MonoBehaviour
{
    private void DestroyAllFridgeItems()
    {
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        FridgeIntroAnimation.fridgeAnimationEnded += DestroyAllFridgeItems;
    }

    private void OnDisable()
    {
        FridgeIntroAnimation.fridgeAnimationEnded -= DestroyAllFridgeItems;
    }

}
