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
        Debug.Log("Levelstart");
        reqCocktail.SetCocktailImage(currentLevelInfo.coctailSprite);
        Debug.Log(requiredCocktailImage.sprite);
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
