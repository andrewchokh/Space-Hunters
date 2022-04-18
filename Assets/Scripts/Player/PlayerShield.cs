using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
	public int health = 1;
	public Image timer;

	private Player player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Update()
	{
		gameObject.transform.position = player.transform.position;
	}

	public void TakeShieldDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Diactivate();
		}
	}

	void Diactivate()
	{
		timer.GetComponent<Image>().fillAmount = 0f;
		timer.enabled = true;
		gameObject.SetActive(false);
	}
}
