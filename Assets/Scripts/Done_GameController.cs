﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public GameObject[] bosses;
	public GameObject[] upgrades;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int bossWave;
	public float upgradeWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private bool restart;
	private bool lockUpdating;
	private int score;
	private int waveCount;
	
	void Start ()
	{
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		waveCount = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnUpgrades ());
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			int numberOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("Boss").Length;

			if (numberOfEnemies == 0) {
				waveCount++;
				if (!restart)
					restartText.text = "Wave " + waveCount;
				if (waveCount % bossWave == 0) {
					GameObject boss = bosses [Random.Range (0, bosses.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (boss, spawnPosition, spawnRotation);
				} 
				else {
					for (int i = 0; i < hazardCount; i++) {
						GameObject hazard = hazards [Random.Range (0, hazards.Length)];
						Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
				}
			}
			yield return new WaitForSeconds (1);
		}
	}

	IEnumerator SpawnUpgrades ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			yield return new WaitForSeconds (upgradeWait);
			GameObject upgrade = upgrades [Random.Range (0, upgrades.Length)];
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (upgrade, spawnPosition, spawnRotation);
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		restartText.text = "Press 'R' for Restart";
		restart = true;
	}
}