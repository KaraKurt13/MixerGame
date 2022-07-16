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
    private void LevelIsCompleted()
    {

    }

    private void LevelIsFailed()
    {

    }

    private void OnEnable()
    {
        CameraIntroAnimation.introFinished += StartLevel;
        
    }
}
