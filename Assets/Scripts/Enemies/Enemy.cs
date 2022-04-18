using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string SpaceshipModel;
    public GameObject shootSound;
    public Transform shotPoint; //The empty game object which will be our weapon muzzle to shoot from
    public GameObject bullet; //Your set-up prefab
    float fireRate; //Fire every 3 seconds
    public float minFireRate;
    public float maxFireRate;
    public float shootingPower; //force of projection
    public float damage;
 
    private float shootingTime; //local to store last time we shot so we can make sure its done every 3s

	public GameObject deathEffect;
    public GameObject deathSound;
    public float health;
    public float speed;
    public int prize;
    private ScoreManager scoreManager;
    public GameObject[] possiblePowerUps;
    public int powerupSpawnChance;
    private int powerupSpawnChancePrivate;
    private Player player;

    void Start()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() 
    {
    	transform.Translate(Vector2.left * speed * Time.deltaTime);

        Fire();

        if (transform.position.x < -11f) 
        {
        	Destroy(gameObject);	
        }	
    }

    public void TakeDamage(float damage) 
    {
    	health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }	
 
    void Fire()
    {
        if(transform.position.x < Random.Range(6f, 7.5f))
        {
            if (Time.time > shootingTime)
            {
                fireRate = Random.Range(minFireRate * 1000, maxFireRate * 1000);
                shootingTime = Time.time + fireRate / 1000; //set the local var. to current time of shooting
                Vector2 myPos = new Vector2(shotPoint.position.x, shotPoint.position.y); //our curr position is where our muzzle points
                bullet.GetComponent<EnemyBullet>().damage = damage; // Set a damage
                GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity); //create our bullet
                Vector2 direction = myPos - (Vector2)gameObject.transform.position; //get the direction to the target
                projectile.GetComponent<Rigidbody2D>().velocity = direction * shootingPower; //shoot the bullet
                Instantiate(shootSound, transform.position, Quaternion.identity);
            }
        }    
    }
 
    void Die()
    {
        powerupSpawnChancePrivate = Random.Range(0, powerupSpawnChance);
        PowerupSpawnChance();
        Instantiate(deathSound, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (player.greakPassive)
        {
            player.greakSpeedPassiveTimer.GetComponent<Image>().fillAmount = 1f;
        }
        scoreManager.AddScore(prize);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TakeDamage(999);
            scoreManager.GetComponent<ScoreManager>().score -= prize;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Meteor")
        {
            Die();
        }
    }

    void PowerupSpawnChance()
    {
        powerupSpawnChancePrivate = Random.Range(0, powerupSpawnChance);
        if (powerupSpawnChancePrivate == powerupSpawnChance - 1)
        {
            int randomPowerup = Random.Range(0, possiblePowerUps.Length);
            if (player.isDamagePowerupActive == false & randomPowerup == 0)
            {
                Instantiate(possiblePowerUps[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (player.isFireRatePowerupActive == false & randomPowerup == 1)
            {
                Instantiate(possiblePowerUps[1], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (player.isImmortalPowerupActive == false & randomPowerup == 2)
            {
                Instantiate(possiblePowerUps[2], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (player.health < player.maxHealth & randomPowerup == 3)
            {
                Instantiate(possiblePowerUps[3], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (player.isSpeedPowerupActive == false & randomPowerup == 4)
            {
                Instantiate(possiblePowerUps[4], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }
}
