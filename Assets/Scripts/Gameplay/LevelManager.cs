using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameProgress gameProgressInfo;
    [SerializeField] private Blender blenderObject;
    [SerializeField] private Image requiredCocktailImage;
    [SerializeField] private GameUIManager UIManager;
    [SerializeField] private LevelInformation currentLevelInfo;
    [SerializeField] private RequiredCocktail reqCocktail;

    [SerializeField] public int currentLevel;

    private void Start()
    {
        currentLevel = gameProgressInfo.currentLevel;
        currentLevelInfo = gameProgressInfo.levelsInfo[currentLevel];
        
    }

    private void StartLevel()
    {
        reqCocktail.SetCocktailImage(currentLevelInfo.coctailSprite);
        blenderObject.requiredColor = currentLevelInfo.requiredColor;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void LevelIsCompleted(int resultScore)
    {
        if(gameProgressInfo.currentLevel+1>gameProgressInfo.levelsInfo.Length-1)
        {
            gameProgressInfo.currentLevel = 0;
            UIManager.ShowWonScreen(resultScore);
            return;
        }
        gameProgressInfo.currentLevel++;
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

    private void OnDisable()
    {
        CameraIntroAnimation.introFinished -= StartLevel;
        Blender.colorIsSimilarToRequired -= LevelIsCompleted;
        Blender.colorIsNotSimilarToRequired -= LevelIsFailed;
    }
}
