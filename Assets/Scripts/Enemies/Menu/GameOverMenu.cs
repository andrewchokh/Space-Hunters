using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text finalScore;
    public ScoreManager sm;

    public GameObject gameOverMusic;
    public GameObject battleMusic;

    public GameObject gameUI;

    public GameObject enemiesSpawner;
    public GameObject meteorSpawner;

    public GameObject loadingScreen;
    public Slider slider;


    void Start()
    {
        battleMusic.SetActive(false);

        gameUI.SetActive(false);
        enemiesSpawner.SetActive(false);
        meteorSpawner.SetActive(false);
        
        if(PlayerPrefs.GetInt("BestScore") < sm.score)
        {
            PlayerPrefs.SetInt("BestScore", sm.score);
        }
            
    	finalScore.text = ("Your score - ") + sm.score;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + sm.score / 100);
    	Destroy(sm);
    }

    public void TryAgain()
    {
    	gameObject.SetActive(false);
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu(int sceneIndex)
    {
        StartCoroutine(LoadSceneProgress(sceneIndex));
    }

    public void Quit()
    {
    	Application.Quit();
    }

    IEnumerator LoadSceneProgress(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            gameObject.SetActive(false);

            yield return null;
        }
    } 
}

