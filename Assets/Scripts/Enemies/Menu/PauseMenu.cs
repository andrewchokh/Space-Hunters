using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public GameObject gameUI;

    public GameObject selectSound;

    public GameObject music;

    public GameObject player;

    public GameObject loadingScreen;
    public Slider slider;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) & !optionsMenu.activeSelf)
        {
        	if(GameIsPaused)
        	{
                Sound();
        		Resume();
        	}

        	else
        	{
                Sound();
        		Pause();
        	}
        }
    }

    public void Resume()
    {
        Sound();
        music.GetComponent<AudioSource>().volume = 1f;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
        gameUI.SetActive(true);
        player.GetComponent<PlayerWeapon>().enabled = true;
    	GameIsPaused = false;
    }

    void Pause()
    {
        Sound();
        music.GetComponent<AudioSource>().volume = 0.3f;
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
        gameUI.SetActive(false);
        player.GetComponent<PlayerWeapon>().enabled = false;
    	GameIsPaused = true;
    }

    public void Restart()
    {
        Sound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Settings()
    {
        Sound();
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackToMenu(int sceneIndex)
    {
        Sound();
        StartCoroutine(LoadSceneProgress(sceneIndex));
    }

    public void Quit()
    {
        Sound();
    	Application.Quit();
    }

    void Sound()
    {
        Instantiate(selectSound, transform.position, Quaternion.identity);
    }

    IEnumerator LoadSceneProgress(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            pauseMenu.SetActive(false);

            yield return null;
        }
    }    
}
