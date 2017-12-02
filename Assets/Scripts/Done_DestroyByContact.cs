﻿using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Upgrade" || other.tag == "Boss")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}


        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameObject.Find("Ally_Ship01").GetComponent<Done_PlayerController>().enabled = false;
            GameObject.Find("Ally_Ship02").GetComponent<Done_PlayerController>().enabled = false;
            gameController.GameOver();
        }

        if (other.tag == "Ally")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }

        gameController.AddScore(scoreValue);

		Destroy (other.gameObject);
		if(!(gameObject.tag == "Boss"))
			{
			Destroy (gameObject);
			}
	}
}