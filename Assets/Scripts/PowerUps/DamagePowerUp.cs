using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePowerUp : MonoBehaviour
{
	public float powerUpSpeed;
    public GameObject pickupSound;

	public int damageBonus;
	public float duraction;
    public GameObject bullet;

    void Start()
    {
        // Set a boolean variable as true
        GameObject.Find("Player").GetComponent<Player>().isDamagePowerupActive = true;
    }

	void Update()
	{
		transform.Translate(Vector2.left * powerUpSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Player"))
    	{
    		StartCoroutine(IncreaseDamage(other));	
    	}
    }

    IEnumerator IncreaseDamage(Collider2D player)
    {
    	// Spawn a effect and play a sound
        Instantiate(pickupSound, transform.position, Quaternion.identity);

    	// Apply effect to the player
    	PlayerWeapon stats = player.GetComponent<PlayerWeapon>();
    	stats.damage += damageBonus;

    	// Disable enable characteristics of "power up" other than script 
    	GetComponent<SpriteRenderer>().enabled = false;
    	GetComponent<Collider2D>().enabled = false;

    	// Wait x amount of seconds
    	yield return new WaitForSeconds(duraction);

    	// Reverse the effect on out player
    	stats.damage -= damageBonus;

        // Set a boolean variabla as false
        player.GetComponent<Player>().isDamagePowerupActive = false;

    	// Remove power up object
    	Destroy(gameObject);
    }
}
