﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public GameObject[] upgrades;
	public Vector3 spawnValues;
	public int hazardCount;
	public int upgradeCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float upgradeWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start () {
		gameOver = false;
		restart = false;
		scoreText.text = "Score: ";
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnUpgrades ());
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	IEnumerator SpawnUpgrades () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			GameObject upgrade = upgrades [Random.Range (0, upgrades.Length)];
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;;
			Instantiate (upgrade, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (upgradeWait);
		}
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
	}
}
