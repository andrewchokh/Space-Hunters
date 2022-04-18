using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyLevelMenu : MonoBehaviour
{
	public GameObject diffLevelMenu;

	public GameObject loadingScreen;
	public Slider slider;

	public GameObject mainMenu;

	public GameObject selectSound;

	public Text levelDesc;

	public Button startButton;

	int currentDiffLevel;
	public Text diffLevelText;
	public Button previousDiffLevelButton;
	public Button nextDiffLevelButton;

	void Update()
	{
		if (PlayerPrefs.GetInt("DifficultyLevel") == 1 || PlayerPrefs.GetInt("DifficultyLevel") == 2 || PlayerPrefs.GetInt("DifficultyLevel") == 3 & PlayerPrefs.GetInt("BestScore") >= 2000)
		{
			startButton.interactable = true;
		}
		else
		{
			startButton.interactable = false;
		}

		if (currentDiffLevel == 1)
		{
			diffLevelText.text = "Easy";
			nextDiffLevelButton.interactable = true;
			previousDiffLevelButton.interactable = false;
		}
		if (currentDiffLevel == 2)
		{
			diffLevelText.text = "Medium";
			nextDiffLevelButton.interactable = true;
			previousDiffLevelButton.interactable = true;
		}
		if (currentDiffLevel == 3)
		{
			diffLevelText.text = "Hard";
			nextDiffLevelButton.interactable = false;
			previousDiffLevelButton.interactable = true;
		}
	}

	public void SetDifficultyLevel(int difficulty)
	{
		Sound();
		PlayerPrefs.SetInt("DifficultyLevel", difficulty);

		if(difficulty == 1)
		{
			levelDesc.text = "For a new players that want to learn the game mechanics or to training.";
		}
		if(difficulty == 2)
		{
			levelDesc.text = "Standart mode. You can feel the game more correctry than in different difficulty levels.";
		}
		if(difficulty == 3 & PlayerPrefs.GetInt("BestScore") >= 2000)
		{
			levelDesc.text = "Hardcore needs if you tired of get a high score in every game on the lower difficulty levels. Pick the most powerful spaceships!";
		}
		if(difficulty == 3 & PlayerPrefs.GetInt("BestScore") < 2000)
		{
			levelDesc.text = "To unlock this level you need to get 2000 points or more.";
		}
	}

	public void StartTheGame(int sceneIndex)
	{
		Sound();
		StartCoroutine(LoadGameProgress(sceneIndex));
		diffLevelMenu.SetActive(false);
	}

	public void BackToMenu()
	{
		Sound();
		PlayerPrefs.SetInt("DifficultyLevel", 0);
		levelDesc.text = "Select the difficulty level to read the description.";
		diffLevelMenu.SetActive(false);
		mainMenu.SetActive(true);
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

    void Sound()
    {
    	Instantiate(selectSound, transform.position, Quaternion.identity);
    }

    public void NextLevel()
    {
    	currentDiffLevel += 1;
    }

    public void PreviousLevel()
    {
    	currentDiffLevel -= 1;
    }
}
