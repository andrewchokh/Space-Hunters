using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRatePowerUp : MonoBehaviour
{
	public float powerUpSpeed;
    public GameObject pickupSound;

	public float fireRateBonus;
	public float duraction;

    void Start()
    {
        // Set a boolean variable as true
        GameObject.Find("Player").GetComponent<Player>().isFireRatePowerupActive = true;
    }

	void Update()
	{
		transform.Translate(Vector2.left * powerUpSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Player"))
    	{
    		StartCoroutine(IncreaseFireRate(other));	
    	}
    }

    IEnumerator IncreaseFireRate(Collider2D player)
    {
    	// Spawn a effect and play a sound
        Instantiate(pickupSound, transform.position, Quaternion.identity);

    	// Apply effect to the player
    	PlayerWeapon stats = player.GetComponent<PlayerWeapon>();
    	stats.fireRate -= fireRateBonus;

    	// Disable enable characteristics of "power up" other than script 
    	GetComponent<SpriteRenderer>().enabled = false;
    	GetComponent<Collider2D>().enabled = false;

    	// Wait x amount of seconds
    	yield return new WaitForSeconds(duraction);

    	// Reverse the effect on out player
    	stats.fireRate += fireRateBonus;

        // Set a boolean variabla as false
        player.GetComponent<Player>().isFireRatePowerupActive = false;

    	// Remove power up object
    	Destroy(gameObject);
    }
}
