using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawner : MonoBehaviour
{
	public GameObject[] enemies;
	public GameObject[] bosses;

	private float[] positions = { 3.75f, -3.75f, 2.5f, -2.5f, 1.25f, -1.25f, 0 }; //3.75f, -3.75f, 2.5f, -2.5f, 1.25f, -1.25f, 0 
	float lastPositionY;

	public Text waveText;

	public GameObject[] existsEnemies;

	int enemyCount;

	int i;

	int currentWave = 0;

	void Start() 
	{
		Spawn();
	}

	IEnumerator SpawnStandartWave() 
	{
		waveText.text = "Wave " + currentWave;

		Animator waveTextAnim = waveText.GetComponent<Animator>();
		waveTextAnim.SetBool("isActive", true);
		yield return new WaitForSeconds(2.5f);
		waveTextAnim.SetBool("isActive", false);

		int enemiesLimit = 10;

		for (enemyCount = 0; enemyCount < enemiesLimit; enemyCount++)
		{
			float randomPosition = positions[Random.Range(0, positions.Length)];

			while (randomPosition == lastPositionY && lastPositionY != null)
			{
				randomPosition = positions[Random.Range(0, positions.Length)];
			}

			lastPositionY = randomPosition;

			GameObject lastEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(11f, randomPosition, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
			existsEnemies = new GameObject[enemyCount + 1];
			existsEnemies[enemyCount] = lastEnemy;

			yield return new WaitForSeconds(Random.Range(1f, 2.5f));
		}

		while(true)
		{
			int killedEnemies = 0;
			int i = 0;
			bool noOneExistsEnemy;

			for (i = 0; i < existsEnemies.Length; i++)
			{
				if (existsEnemies[i] == null)
				{
					killedEnemies ++;
				}
			}

			if (killedEnemies == existsEnemies.Length)
			{
				Spawn();
				break;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator SpawnBoss()
	{
		if (currentWave == 10)
		{
			waveText.text = "Wave " + currentWave + "\n The Boss is approaching!";

			Animator waveTextAnim = waveText.GetComponent<Animator>();
			waveTextAnim.SetBool("isActive", true);
			yield return new WaitForSeconds(2.5f);
			waveTextAnim.SetBool("isActive", false);

			Instantiate(bosses[0], new Vector3(13f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
		}	
	}

	void Spawn()
	{
		currentWave ++;

		if (currentWave < 10)
		{
			StartCoroutine(SpawnStandartWave());
			Debug.Log("wave " + currentWave);
		}
		else if (currentWave == 10)
		{
			SpawnBoss();
			Debug.Log("wave " + currentWave + "\n Boss wave!");
		}
	}
}
