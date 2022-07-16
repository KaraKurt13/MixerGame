using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInformation[] levelInfo;
    [SerializeField] private Blender blenderObject;
    [SerializeField] private Image requiredCocktailImage;
    [SerializeField] private GameUIManager UIManager;

    public int currentLevel = 0;

    private void StartLevel()
    {
        requiredCocktailImage.sprite = levelInfo[currentLevel].coctailSprite;
        blenderObject.requiredColor = levelInfo[currentLevel].requiredColor;
    }

    private void RestartLevel()
    {
        blenderObject.ClearBlender();
        StartLevel();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void LevelIsCompleted(int resultScore)
    {
        currentLevel++;
        UIManager.ShowWonScreen(resultScore);
    }

    private void LevelIsFailed(int resultScore)
    {
        UIManager.ShowLostScreen(resultScore);
    }

    private void OnEnable()
    {
        CameraIntroAnimation.introFinished += StartLevel;
        Blender.colorIsSimilarToRequired += LevelIsCompleted;
        Blender.colorIsNotSimilarToRequired += LevelIsFailed;
        
    }
}
