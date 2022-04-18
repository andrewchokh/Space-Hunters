using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject optionsMenu;
    public GameObject shopMenu;
    public GameObject diffLevelMenu;

	public GameObject selectSound;

    public Text bestScoreText;

    public GameObject loadingScreen;
    public Slider slider;

    public Image playerCurrentSkin;
    public ShopMenu shopMenuInfo;

    public Button achievementsButton;

    void Awake()
    {
        Time.timeScale = 1f;
        achievementsButton.interactable = false;
    }  

    public void Play()
    {
        Sound();
        StartCoroutine(LoadGameProgress(1));
        mainMenu.SetActive(false);
    }

    public void Options()
    {
    	Sound();
    	mainMenu.SetActive(false);
    	optionsMenu.SetActive(true);
    }

    public void Shop()
    {
        Sound();
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
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

    void Update()
    {
        playerCurrentSkin.sprite = shopMenuInfo.skins[PlayerPrefs.GetInt("CurrentSkin")];

        bestScoreText.text = "Best score - " + PlayerPrefs.GetInt("BestScore", 0);
    }

    IEnumerator LoadGameProgress(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            mainMenu.SetActive(false);

            yield return null;
        }
    }
}
