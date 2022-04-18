using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner1 : MonoBehaviour
{
	public GameObject[] enemies;
	private float[] positions = { 3.75f,-3.75f, 2.5f, -2.5f, 1.25f, -1.25f, 0 }; //3.75f,-3.75f, 2.5f, -2.5f, 1.25f, -1.25f, 0 

	void Start() 
	{
		StartSpawn();
	}

	IEnumerator spawn() 
	{
		while(true) {
			yield return new WaitForSeconds(Random.Range(1f, 3f));
			Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(11f, positions[Random.Range(0, positions.Length)], 0), Quaternion.Euler(new Vector3(0, 0, 0)));
		}
	}

	public void StartSpawn()
	{
		StartCoroutine(spawn());
	}
}
