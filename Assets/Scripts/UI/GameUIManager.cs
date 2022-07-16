using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject wonScreen;
    [SerializeField] private TextMeshProUGUI wonResult;
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private TextMeshProUGUI lostResult;

    private void ShowGameUI()
    {
        gameUI.SetActive(true);
    }

    public void ShowWonScreen(int similarityPercentage)
    {
        wonScreen.SetActive(true);
        StartCoroutine(ShowWonResult(similarityPercentage));
        
    }

    public void ShowLostScreen(int similarityPercentage)
    {
        lostScreen.SetActive(true);
        StartCoroutine(ShowLostResult(similarityPercentage));
    }

    private IEnumerator ShowWonResult(int result)
    {
        for(int i=0;i<=result;i++)
        {
            wonResult.text = i.ToString();
            yield return new WaitForSeconds(0.06f);
        }

        wonResult.text += "%";
    }

    private IEnumerator ShowLostResult(int result)
    {
        for (int i = 0; i <= result; i++)
        {
            lostResult.text = i.ToString();
            yield return new WaitForSeconds(0.06f);
        }

        lostResult.text += "%";
    }


    private void OnEnable()
    {
        CameraIntroAnimation.introFinished += ShowGameUI;
    }
}
