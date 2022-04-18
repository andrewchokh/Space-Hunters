using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StitchBulletPowerup : MonoBehaviour
{
	public float powerUpSpeed;
    public GameObject pickupSound;

	public float duraction;

    void Start()
    {
        // Set a boolean variable as true
        //GameObject.Find("Player").GetComponent<Player>().isSpeedPowerupActive = true;
    }

	void Update()
	{
		transform.Translate(Vector2.left * powerUpSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Player"))
    	{
    		StartCoroutine(IncreaseSpeed(other));	
    	}
    }

    IEnumerator IncreaseSpeed(Collider2D player)
    {
    	// Spawn a effect and play a sound
        Instantiate(pickupSound, transform.position, Quaternion.identity);

    	// Apply effect to the player
    	PlayerWeapon bulletStats = player.GetComponent<PlayerWeapon>();
    	bulletStats.isStitch = true;

    	// Disable enable characteristics of "power up" other than script 
    	GetComponent<SpriteRenderer>().enabled = false;
    	GetComponent<Collider2D>().enabled = false;

    	// Wait x amount of seconds
    	yield return new WaitForSeconds(duraction);

    	// Reverse the effect on out player
    	bulletStats.isStitch = false;

    	// Remove power up object
    	Destroy(gameObject);
    }
}
