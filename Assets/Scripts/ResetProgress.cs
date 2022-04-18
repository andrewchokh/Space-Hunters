using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgress : MonoBehaviour
{
	public Text BestScoreText;
	public Text CoinsText;

	public GameObject resetSound;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) & Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.RightControl) & Input.GetKeyDown(KeyCode.R))
        {
        	// Delete the Best Score
        	PlayerPrefs.SetInt("BestScore", 0);

        	// Delete the money
        	PlayerPrefs.SetInt("Coins", 0);
        	PlayerPrefs.SetInt("AddCoins", 0);

        	// Delete a skins
        	PlayerPrefs.SetInt("CurrentSkin", 0);
        	PlayerPrefs.SetInt("SabreSkin", 0);
            PlayerPrefs.SetInt("GreakSkin", 0);
            PlayerPrefs.SetInt("DuoCopterSkin", 0);

        	// Play a sound
        	Instantiate(resetSound, transform.position, Quaternion.identity);
        }
    }
}
