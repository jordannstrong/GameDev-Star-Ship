using System.Collections;
using UnityEngine;

public class Done_UpgradebyContact : MonoBehaviour {
	public int upgradeType;

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			GameObject player = other.transform.gameObject;
			player.GetComponent<Done_PlayerController> ().shotType = this.upgradeType;
			Destroy (gameObject);
		}
	}
}
