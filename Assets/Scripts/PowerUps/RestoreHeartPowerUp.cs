using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHeartPowerUp : MonoBehaviour
{
	public float powerUpSpeed;
    public GameObject pickupSound;

	void Update()
	{
		transform.Translate(Vector2.left * powerUpSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Player"))
    	{
    		RestoreHp(other);	
    	}
    }

    void RestoreHp(Collider2D player)
    {
    	// Spawn a effect and play a sound
        Instantiate(pickupSound, transform.position, Quaternion.identity);

    	// Apply effect to the player
    	Player stats = player.GetComponent<Player>();
    	stats.health += 0.4f;

    	// Remove power up object
    	Destroy(gameObject);
    }
}
