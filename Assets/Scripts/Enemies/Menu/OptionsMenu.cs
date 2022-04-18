using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Работа с интерфейсами
using UnityEngine.SceneManagement; //Работа со сценами
using UnityEngine.Audio; //Работа с аудио
using System.Linq;

public class OptionsMenu : MonoBehaviour
{
	// Back to menu
	public GameObject otherMenu;
	public GameObject optionsMenu;

	// Music and sound
    public AudioMixerGroup Mixer;
    public Slider musicVolume;
    public Slider soundVolume;
    public Toggle musicToggle;
    public Toggle soundToggle;

    // Quality
    public Dropdown qualitiesList;
    Resolution[] res;

    // FullScreen Mode
    public Toggle fullscreenCheckbox;

    // Screen resolution
    public Dropdown resolutionsList;
    private bool isFullscreenResolution;

    // Select sound
    public GameObject selectSound;

    void Start()
    {
        //GetComponentInChildren<Dropdown>().options = PlayerPrefs.GetInt("Quality", 0);

        // Get the resolutions and add them to dropdown
        Resolution[] resolution = Screen.resolutions;
        res = resolution.Distinct().ToArray();
        string[] strRes = new string[res.Length];
        for (int i = 0; i < res.Length; i++)
        {
            //strRes[i] = res[i].ToString();
            strRes[i] = res[i].width.ToString() + "x" + res[i].height.ToString();
        }
        resolutionsList.ClearOptions();
        resolutionsList.AddOptions(strRes.ToList());

        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        soundVolume.value = PlayerPrefs.GetFloat("SoundsVolume", 0);
        resolutionsList.value = PlayerPrefs.GetInt("Resolution", res.Length - 1);
        musicToggle.isOn = PlayerPrefs.GetInt("IsMusicOn", 0) == 0;
        soundToggle.isOn = PlayerPrefs.GetInt("IsSoundOn", 0) == 0;
        fullscreenCheckbox.isOn = PlayerPrefs.GetInt("Fullscreen", 0) == 0;
        if (Screen.fullScreen)
        {
            fullscreenCheckbox.isOn = true;
        }
        else
        {
            fullscreenCheckbox.isOn = false;
        }
        qualitiesList.value = PlayerPrefs.GetInt("Quality", 0);
    }

    public void BackToMenu()
    {
        Sound();
    	otherMenu.SetActive(true);
    	optionsMenu.SetActive(false);
    }

    public void SetMusicVolume(float mVolume)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", mVolume);
        PlayerPrefs.SetFloat("MusicVolume", mVolume);
    }

    public void SetSoundsVolume(float sVolume)
    {
        Mixer.audioMixer.SetFloat("SoundsVolume", sVolume);
        PlayerPrefs.SetFloat("SoundsVolume", sVolume);
    }

    public void SetMusicActive()
    {
        if (!musicToggle.isOn)
        {
            musicVolume.interactable = false;
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetInt("IsMusicOn", 1);
        }
        else
        {
            musicVolume.interactable = true;
            Mixer.audioMixer.SetFloat("MusicVolume", musicVolume.value);
            PlayerPrefs.SetInt("IsMusicOn", 0);
        }
    }

    public void SetSoundActive()
    {
        if (!soundToggle.isOn)
        {
            soundVolume.interactable = false;
            Mixer.audioMixer.SetFloat("SoundsVolume", -80);
            PlayerPrefs.SetInt("IsSoundOn", 1);
        }
        else
        {
            Sound();
            soundVolume.interactable = true;
            Mixer.audioMixer.SetFloat("SoundsVolume", soundVolume.value);
            PlayerPrefs.SetInt("IsSoundOn", 0);
        }
    }

    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(qualitiesList.value, Screen.fullScreen);
        PlayerPrefs.SetInt("Quality", qualitiesList.value);
    }

    public void SetFullscreen()
    {
        if (!fullscreenCheckbox.isOn)
        {
            Screen.fullScreen = false;
            PlayerPrefs.GetInt("Fullscreen", 1);
        }
        else
        {
            Screen.fullScreen = true;
            PlayerPrefs.GetInt("Fullscreen", 0);
        }
    }    

    public void SetResolution()
    {
        Screen.SetResolution(res[resolutionsList.value].width, res[resolutionsList.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionsList.value);
    }

    void Sound()
    {
        Instantiate(selectSound, transform.position, Quaternion.identity);
    }
}		
