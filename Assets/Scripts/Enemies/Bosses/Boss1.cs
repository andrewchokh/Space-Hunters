using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
	public string SpaceshipModel;
    public GameObject shootSound;
    public Transform[] shotPoints; //The empty game object which will be our weapon muzzle to shoot from
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
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeAttack());
    }

    IEnumerator Shelling()
    {
    	int rebound = 0;

    	while(true)
    	{
    		transform.Translate(Vector2.up * speed * Time.deltaTime);

        	if (transform.position.y >= 3.75f || transform.position.y <= -3.75f) 
        	{
        		speed = -speed;
        		rebound ++;
        	}

        	if (rebound >= 10 & Mathf.Round(transform.position.y) == 0f)
        	{
        		break;
        	}

        	yield return null;
    	}

        StartCoroutine(ChangeAttack());
    }

    IEnumerator ChangeAttack()
    {
        Debug.Log("Waiting...");
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        int randomAttack = Random.Range(0, 1);

        if (randomAttack == 0)
        {
            StartCoroutine(Shelling());
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

    void Die()
    {
    	Destroy(gameObject);
    }
}
