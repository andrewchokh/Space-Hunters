using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerSpawn : MonoBehaviour
{
	public GameObject meteor;
	private float[] meteorPositions = { 3f, -3f, 0 }; //3.75f,-3.75f, 2.5f, -2.5f, 1.25f, -1.25f, 0 

	private float minRangeOfTimeToAction = 30f;
	private float maxRangeOfTimeToAction = 40f;

	private float minRangeOfTimeToMeteorShower = 0.7f;
	private float maxRangeOfTimeToMeteorShower = 1f;

	private float i;

	private ScoreManager sm;
	private bool eventsIsActive = false;

	public GameObject warningObject;

	public GameObject enemiesSpawner;

	void Start()
	{
		Meteor meteorStats = meteor.GetComponent<Meteor>();
		sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

		if ((PlayerPrefs.GetInt("DifficultyLevel") == 1))
		{
			gameObject.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("DifficultyLevel") == 2)
		{
			meteorStats.speed = 10f;
			meteorStats.health = 1;
		}
		else if (PlayerPrefs.GetInt("DifficultyLevel") == 3)
		{
			meteorStats.speed = 15f;
			meteorStats.health = Random.Range(1, 3);
		}
	}

	void Update()
	{
		if (sm.score >= Random.Range(1, 2) & !eventsIsActive)
		{
			StartCoroutine(SpawnEvent());
		}
	}

	IEnumerator SpawnEvent()
	{
		eventsIsActive = true;

		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minRangeOfTimeToAction, maxRangeOfTimeToAction)); 
			StartCoroutine(ShowWarning());
		}	
	}

	IEnumerator MeteorShower() 
	{
		for (int i = 0; i < Random.Range(10, 20); i++) 
		{
			yield return new WaitForSeconds(Random.Range(minRangeOfTimeToMeteorShower, maxRangeOfTimeToMeteorShower));
			Instantiate(meteor, new Vector3(12f, meteorPositions[Random.Range(0, meteorPositions.Length)], 0), Quaternion.Euler(new Vector3(0, 0, 0)));
		}
		enemiesSpawner.SetActive(true);
	}

	IEnumerator ShowWarning()
	{
		warningObject.SetActive(true);
		yield return new WaitForSeconds(3.5f);
		warningObject.SetActive(false);
		enemiesSpawner.SetActive(false);
		StartCoroutine(MeteorShower());
	}
}
