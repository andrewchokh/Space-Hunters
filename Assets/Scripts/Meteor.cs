using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	public float health;
	public float speed;

    public GameObject destroyParticle;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x < -14f) {
        	Destroy(gameObject);	
        }	
    }

    public void TakeDamage(float damage) {
    	health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }	  

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            PlayerBullet bullet = other.gameObject.GetComponent<PlayerBullet>();
            TakeDamage(bullet.damage);
            Instantiate(bullet.hitParticle, transform.position, Quaternion.identity);
            Instantiate(bullet.hitSound, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
    }  

    void Die()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }  
}
