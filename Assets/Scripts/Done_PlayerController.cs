using UnityEngine;
using System.Collections;

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

	public GameObject shot;
	public Transform shotSpawn01;
    public Transform shotSpawn02;
    public Transform railgunShot;
    public GameObject railGun;
	public float fireRate;
    public float railChargeRate;
	 
	private float nextFire;
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn01.position, shotSpawn01.rotation);
            Instantiate(shot, shotSpawn02.position, shotSpawn02.rotation);
            GetComponent<AudioSource>().Play ();
		}
        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + railChargeRate;
            Instantiate(railGun, railgunShot.position, railgunShot.rotation);
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
