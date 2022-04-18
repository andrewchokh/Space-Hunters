using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    public float speed;

    public float health; 
    public Slider healthBar;
    public float maxHealth;
    //public Sprite heart;
    //public Sprite emptyHeart;
    //public int numOfHearts;
    //public Image[] hearts;

    public bool isDamagePowerupActive = false;
    public bool isFireRatePowerupActive = false;
    public bool isSpeedPowerupActive = false;
    public bool isImmortalPowerupActive = false;

    public bool standartPassive = false;
    public bool sabrePassive = false;
    public bool greakPassive = false;
    public bool duoCopterPassive = false;

    public bool isImmortal = false;
    public Sprite[] standartSprites;
    public Sprite[] immortalSprites;

    public GameObject deathEffect;
    public GameObject deathSound;

    public GameObject gameOverMenu;

    Animator camAnim;

    public GameObject shield;
    public bool isProtectedByShield;
    public Image shieldTimer;
    public GameObject activeShieldSound;
    public GameObject disableShieldSound;

    public Image greakSpeedPassiveTimer;
    private bool greakPassiveIsActive;

    public Image duoCopterFireRateTimer;
    private bool duoCopterPassiveIsActive;

    void Start() 
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>(); 
        healthBar.maxValue = health;
        maxHealth = health;
    }

    void Update()
    {
        healthBar.value = health;

        if (standartPassive)
        {
            if (shieldTimer.GetComponent<Image>().fillAmount == 0f)
            {
                shield.SetActive(false);
                isProtectedByShield = false;
                Instantiate(disableShieldSound, transform.position, Quaternion.identity);
                shieldTimer.GetComponent<Timer>().duraction = 90;
                shieldTimer.GetComponent<Timer>().mode = 1;
            }
        }
        else
        {
            Destroy(shield);
            Destroy(shieldTimer);
        }    

        if (greakPassive)
        {
            if (greakSpeedPassiveTimer.GetComponent<Image>().fillAmount == 1f & !duoCopterPassiveIsActive)
            {
                speed -= 0.5f;
                duoCopterPassiveIsActive = true;
            }
            else if (greakSpeedPassiveTimer.GetComponent<Image>().fillAmount != 1f & duoCopterPassiveIsActive)
            {
                speed += 0.5f;
                duoCopterPassiveIsActive = false;
            }
        }
        else
        {
            Destroy(greakSpeedPassiveTimer);
        }

        if (duoCopterPassive)
        {
            if (duoCopterFireRateTimer.GetComponent<Image>().fillAmount == 1f & !greakPassiveIsActive)
            {
                GetComponent<PlayerWeapon>().fireRate -= 0.5f;
                greakPassiveIsActive = true;
            }
            else if (duoCopterFireRateTimer.GetComponent<Image>().fillAmount != 1f & greakPassiveIsActive)
            {
                GetComponent<PlayerWeapon>().fireRate += 0.5f;
                greakPassiveIsActive = false;
            }
        }    
        else
        {
            Destroy(duoCopterFireRateTimer);
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;

        if (health <= 0)
        {
            if (standartPassive)
            {    
                if (shieldTimer.GetComponent<Image>().fillAmount != 1f)
                {
                    Die();
                }    
                else if (shieldTimer.GetComponent<Image>().fillAmount == 1f)
                {
                    health = 1;
                    shield.SetActive(true);
                    Instantiate(activeShieldSound, transform.position, Quaternion.identity);
                    isProtectedByShield = true;
                    shieldTimer.GetComponent<Timer>().duraction = 4;
                    shieldTimer.GetComponent<Timer>().mode = 0;
                }
            }  
            else
            {
                Die();
            }  
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Instantiate(deathSound, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);       
        camAnim.SetTrigger("shake");
        isDamagePowerupActive = false;
        isFireRatePowerupActive = false;
        isSpeedPowerupActive = false;
        isImmortalPowerupActive = false;
        gameOverMenu.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!isProtectedByShield & !isImmortal)
            {
                Die();
            }
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Meteor")
        {
            if (!isImmortal)
            {
                Die();
            }
        }
    }
}
   