using UnityEngine;
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
	
	private bool gameOver;
	private bool restart;
	private int score;
	private int waveCount;
	
	void Start ()
	{
		gameOver = false;
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
			int numberOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;

			if (numberOfEnemies == 0) {
				waveCount++;
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
		gameOver = true;
		restart = true;
	}
}