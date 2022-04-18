using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public GameObject hitParticle;
    public GameObject hitSound;
    
    void Update()
    {	   		
    	if (transform.position.x <= -11f)
    	{
    		Destroy(gameObject);
    	}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (!player.isProtectedByShield & !player.isImmortal)
            {
                player.TakeDamage(damage);
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                Instantiate(hitSound, transform.position, Quaternion.identity);
                Destroy(gameObject);

                if (player.duoCopterPassive)
                {
                    player.duoCopterFireRateTimer.GetComponent<Image>().fillAmount = 0f;
                }
            }
            else if (player.isProtectedByShield || player.isImmortal)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                Instantiate(hitSound, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
