using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float shootingPower;
    public float damage;
    public GameObject hitParticle;
    public GameObject hitSound;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * shootingPower;
    }

    void Update()
    {
    	if(transform.position.x >= 11f)
    	{
    		Destroy(gameObject);
    	}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Instantiate(hitSound, transform.position, Quaternion.identity);
            Destroy(gameObject);  
        }
        else if (other.gameObject.tag == "Meteor")
        {
            Meteor meteor = other.gameObject.GetComponent<Meteor>();
            meteor.TakeDamage(damage);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Instantiate(hitSound, transform.position, Quaternion.identity);
            Destroy(gameObject);  
        }
    }
}
