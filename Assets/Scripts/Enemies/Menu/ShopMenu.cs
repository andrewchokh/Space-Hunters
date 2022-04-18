using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopMenu : MonoBehaviour
{
    public GameObject otherMenu;
	public GameObject shopMenu;

	public GameObject selectSound;

	public Button nextButton;
	public Button previousButton;

	public Sprite[] skins;
	private int currentSkin;
	public Image skin;

	public Text nameText;
	public Text descriptionText;
	public Button buyButton;
	public Text textButtonPrice;
	public Text numOfMoneyText;

	public GameObject buySound;
	public GameObject declineSound;

	public void BackToMenu()
	{
		Sound();
    	otherMenu.SetActive(true);
    	shopMenu.SetActive(false);
	}

	void Start()
	{
		currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
	}

	void Sound()
    {
        Instantiate(selectSound, transform.position, Quaternion.identity);
    }

    void Update()
    {
    	numOfMoneyText.text = "× " + PlayerPrefs.GetInt("Coins");

    	if(currentSkin == 0)
    	{
    		previousButton.interactable = false;
    		nextButton.interactable = true;

    		skin.sprite = skins[0];

    		nameText.text = "Standart";
    		descriptionText.text = "Def: 4" + "\n" + "Damage: 1" + "\n" + "Fire Rate: 1" + "\n" + "Speed: 5" + "\n" + "Type: Medium " + "\n" + "Passive: When the player has 1 hp, a shield is activated that absorbs damage for 5 seconds. (Reload: 90s).";

    		buyButton.interactable = false;
    		textButtonPrice.text = "Acquired";
    		PlayerPrefs.SetInt("CurrentSkin", currentSkin);
    	}

    	else if(currentSkin == 1)
    	{
    		nextButton.interactable = true;
    		previousButton.interactable = true;

    		skin.sprite = skins[1];
    		nameText.text = "Sabre";
    		descriptionText.text = "Def: 3" + "\n" + "Damage: 2" + "\n" + "Fire Rate: 0.8" + "\n" + "Speed: 6.5" + "\n" + "Type: Lightweight " + "\n" + "Passive: Every third bullet always have an additional damage.";

    		if (PlayerPrefs.GetInt("SabreShip") == 1);
    		{
    			buyButton.interactable = false;
    			textButtonPrice.text = "Acquired";
    		}

    		if (PlayerPrefs.GetInt("SabreShip") != 1)
    		{
    			buyButton.interactable = true;
    			textButtonPrice.text = "200";
    		}

    		if(textButtonPrice.text == "Acquired")
    		{
    			PlayerPrefs.SetInt("CurrentSkin", currentSkin);
    		}
    	}

        else if(currentSkin == 2)
        {
            nextButton.interactable = true;
            previousButton.interactable = true;

            skin.sprite = skins[2];
            nameText.text = "Greak";
            descriptionText.text = "Def: 5" + "\n" + "Damage: 2" + "\n" + "Fire Rate: 1.5" + "\n" + "Speed: 3" + "\n" + "Type: Heavy " + "\n" + "Passive: After killing the speed increases by 1.5 for 1 second.";

            if (PlayerPrefs.GetInt("GreakShip") == 1);
            {
                buyButton.interactable = false;
                textButtonPrice.text = "Acquired";
            }

            if (PlayerPrefs.GetInt("GreakShip") != 1)
            {
                buyButton.interactable = true;
                textButtonPrice.text = "300";
            }

            if(textButtonPrice.text == "Acquired")
            {
                PlayerPrefs.SetInt("CurrentSkin", currentSkin);
            }
        }

        else if(currentSkin == 3)
        {
            nextButton.interactable = false;
            previousButton.interactable = true;

            skin.sprite = skins[3];
            nameText.text = "Duo-Copter";
            descriptionText.text = "Def: 3" + "\n" + "Damage: 2" + "\n" + "Fire Rate: 1.5" + "\n" + "Speed: 6.5" + "\n" + "Type: Lightweight " + "\n" + "Passive: If the player does not take any damage within 5 seconds, his fire rate will reduce by 1.";

            if (PlayerPrefs.GetInt("DuoCopterShip") == 1);
            {
                buyButton.interactable = false;
                textButtonPrice.text = "Acquired";
            }

            if (PlayerPrefs.GetInt("DuoCopterShip") != 1)
            {
                buyButton.interactable = true;
                textButtonPrice.text = "300";
            }

            if(textButtonPrice.text == "Acquired")
            {
                PlayerPrefs.SetInt("CurrentSkin", currentSkin);
            }
        }

    }

    public void NextSkin()
    {
    	currentSkin += 1;
    }

    public void PreviousSkin()
    {
    	currentSkin -= 1;
    }

    public void BuySkin()
    {
    	if(PlayerPrefs.GetInt("Coins") >= Int32.Parse(textButtonPrice.text))
    	{
    		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Int32.Parse(textButtonPrice.text));
    		Instantiate(buySound, transform.position, Quaternion.identity);

            if (currentSkin == 1)
            {
                PlayerPrefs.SetInt("SabreShip", 1);
            }
            else if (currentSkin == 2)
            {
                PlayerPrefs.SetInt("GreakShip", 1);
            }
            else if (currentSkin == 3)
            {
                PlayerPrefs.SetInt("DuoCopterShip", 1);
            }
    	}
    	else
    	{
    		Instantiate(declineSound, transform.position, Quaternion.identity);
    	}
    }
}
