using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    public Sprite[] playerSkins;
    public GameObject player;

    public GameObject shotPoint;
    public GameObject bullet;

    void Awake()
    {
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        Player playerStats = player.GetComponent<Player>();
        PlayerWeapon playerWeapon = player.GetComponent<PlayerWeapon>();

        playerSprite.sprite = playerSkins[PlayerPrefs.GetInt("CurrentSkin")];

    	if(PlayerPrefs.GetInt("CurrentSkin") == 0)
    	{
            playerStats.health = 4;
            playerStats.speed = 5f;
            playerWeapon.damage = 1;
            playerWeapon.fireRate = 1f;
            playerWeapon.shootingPower = 30f;
            playerStats.standartPassive = true;
    	}
    	else if(PlayerPrefs.GetInt("CurrentSkin") == 1)
    	{
            playerStats.health = 3;
            playerStats.speed = 6.5f;
            playerWeapon.damage = 2;
            playerWeapon.fireRate = 1.2f;
            playerWeapon.shootingPower = 30f; 
            playerStats.sabrePassive = true;
    	}
        else if(PlayerPrefs.GetInt("CurrentSkin") == 2)
        {
            playerStats.health = 5;
            playerStats.speed = 3.5f;
            playerWeapon.damage = 2;
            playerWeapon.fireRate = 1.5f;
            playerWeapon.shootingPower = 35f;
            playerStats.greakPassive = true;
        }
        else if(PlayerPrefs.GetInt("CurrentSkin") == 3)
        {
            playerStats.health = 3;
            playerStats.speed = 6.5f;
            playerWeapon.damage = 1;
            playerWeapon.fireRate = 0.8f;
            playerWeapon.shootingPower = 25f;
            playerStats.duoCopterPassive = true;
        }

    	else
    	{
    		Debug.LogError("Cannot find the current skin of player");
    	}

    	Destroy(gameObject);
    }
}
