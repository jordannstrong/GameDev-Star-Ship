using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject regularShot;
	public GameObject railgunShot;
	public GameObject railGunShotVisible;
	public Transform shotSpawners;
	public float fireRate;
    public float railChargeRate;
	public int shotType;

	private float nextRegularFire = 0.0f;
	private float nextRailFire = 0.0f;
	private Transform reg_single_spawner;
	private Transform rail_single_spawner;
	private List<Transform> reg_double_spawners = new List<Transform> ();
	private List<Transform> reg_triple_spawners = new List<Transform> ();


	void Start () {
		for (int spawnerIndex = 0; spawnerIndex < shotSpawners.childCount; spawnerIndex++) {
			Transform spawner = shotSpawners.GetChild(spawnerIndex);
			if (spawner.tag == "Single Shot")
				reg_single_spawner = spawner;
			else if (spawner.tag == "Double Shot")
				reg_double_spawners.Add(spawner);
			else if (spawner.tag == "Triple Shot")
				reg_triple_spawners.Add(spawner);
			else if (spawner.tag == "Railgun Shot")
				rail_single_spawner = spawner;
		}
	}

	void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextRegularFire) {
			nextRegularFire = Time.time + fireRate;

			switch (shotType) {
			case 1: 
				Instantiate (regularShot, reg_single_spawner.position, reg_single_spawner.rotation);
				break;
			case 2:
				foreach (Transform spawner in reg_double_spawners)
					Instantiate (regularShot, spawner.position, spawner.rotation);
				break;
			case 3:
				foreach (Transform spawner in reg_triple_spawners)
					Instantiate (regularShot, spawner.position, spawner.rotation);
				break;
			}
			GetComponent<AudioSource>().Play ();
		}
        if (Input.GetButton("Fire2") && Time.time > nextRailFire) {
			nextRailFire = Time.time + railChargeRate;
			Instantiate(railgunShot, rail_single_spawner.position, rail_single_spawner.rotation);
			Instantiate(railGunShotVisible, rail_single_spawner.position, rail_single_spawner.rotation);
			GetComponent<AudioSource>().Play();
        }
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Vertical");
		float moveVertical = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (-moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)

		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, GetComponent<Rigidbody>().velocity.x* -tilt, 0.0f);
	}
}
