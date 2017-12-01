using UnityEngine;
using System.Collections;

public class Done_BossController : MonoBehaviour
{
	public GameObject explosionKill;
	public GameObject explosionHit;
	public int scoreValue;
	public int hitpoints;
	private Done_GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "PlayerBolt") {
			hitpoints--;
			Destroy (other.gameObject);
			if (explosionHit != null) {
				Instantiate (explosionHit, transform.position, transform.rotation);
			}
		}

		if (hitpoints == 0) {
			Destroy (gameObject);
			Destroy (other.gameObject);
			if (explosionKill != null) {
				Instantiate (explosionKill, transform.position, transform.rotation);
			}
		}
	}
}
