using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float shootingPower;

    public Transform shotPoint;
    public GameObject bullet;
    public GameObject shootSound;
    public bool shootReady = true;

    private bool thirdBulletIsCrit = false;
    private int toCrit = 2; 
    public bool critBullet = false;
    public Image critImage;

    private bool greakShotpoint = false;
    public Transform[] greakShotpoints;

    public bool isStitch = false;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.sabrePassive)
        {
            thirdBulletIsCrit = true;
        }
    }

    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.Z) && shootReady == true)
    	{
            if (PlayerPrefs.GetInt("CurrentSkin") != 1 & PlayerPrefs.GetInt("CurrentSkin") != 2 & shootReady == true)
            {
                PlayerBullet bulletStats = bullet.GetComponent<PlayerBullet>();
                bulletStats.damage = damage;
                bulletStats.shootingPower = shootingPower;
        		Instantiate(bullet, shotPoint.transform.position, Quaternion.identity);
    	    	Instantiate(shootSound, transform.position, Quaternion.identity);
      			shootReady = false;
      			StartCoroutine(Alarm());
            }
            
            // Critical damage (passive) 
            if (thirdBulletIsCrit)
            {
                if (toCrit != 0 & shootReady == true)
                {
                    PlayerBullet bulletStats = bullet.GetComponent<PlayerBullet>();
                    bulletStats.damage = damage;
                    bulletStats.shootingPower = shootingPower;
                    Instantiate(bullet, shotPoint.transform.position, Quaternion.identity);
                    Instantiate(shootSound, transform.position, Quaternion.identity);
                    shootReady = false;
                    critBullet = false;
                    toCrit --;
                    StartCoroutine(Alarm());
                }    

                else if (toCrit == 0 & shootReady == true)
                {
                    PlayerBullet bulletStats = bullet.GetComponent<PlayerBullet>();
                    bulletStats.damage = damage + 2;
                    bulletStats.shootingPower = shootingPower;
                    Instantiate(bullet, shotPoint.transform.position, Quaternion.identity);
                    Instantiate(shootSound, transform.position, Quaternion.identity);
                    shootReady = false;
                    critBullet = true;
                    toCrit = 2; 
                    StartCoroutine(Alarm());
                } 
            }    
            // Critical damage (passive) 

            // Greak bullets 
            else if (PlayerPrefs.GetInt("CurrentSkin") == 2 & shootReady == true)
            {
                PlayerBullet bulletStats = bullet.GetComponent<PlayerBullet>();
                bulletStats.damage = damage;
                bulletStats.shootingPower = shootingPower;
                Instantiate(bullet, greakShotpoints[0].transform.position, Quaternion.identity);
                Instantiate(bullet, greakShotpoints[1].transform.position, Quaternion.identity);
                Instantiate(shootSound, transform.position, Quaternion.identity);
                shootReady = false;
                StartCoroutine(Alarm());
            }
            // Greak bullets 
    	}

        // Critical damage (passive) 
        if (thirdBulletIsCrit)
        {
            if (toCrit == 0) 
            {
                critImage.enabled = true;
            }            
            else
            {
                critImage.enabled = false;
            }
        } 
        else
        {
            Destroy(critImage);
        }   
        // Critical damage (passive) 
    }

    IEnumerator Alarm()
    {
    	yield return new WaitForSeconds(fireRate);
    	shootReady = true;
    	StopCoroutine(Alarm());
    }
}
