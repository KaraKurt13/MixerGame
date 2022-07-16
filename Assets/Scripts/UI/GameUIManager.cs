using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowGameUI()
    {
        gameUI.SetActive(true);
    }

    private void OnEnable()
    {
        CameraIntroAnimation.introFinished += ShowGameUI;
    }
}
