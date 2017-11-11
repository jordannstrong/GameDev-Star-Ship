using UnityEngine;
using System.Collections;

public class move2 : MonoBehaviour
{
	public enum MoveType
	{
		worldAxis,
		localAxis
	}
	public MoveType moveType;
	public float moveSpeed = 1.0f;
	public float rotationSpeedOnLocalAxis = 15.0f;
	float angle;
	float x;
	float z;

	void Awake()
	{
		StartCoroutine ("ChildManager");
	}

	void Update ()
	{
		//
		x = Input.GetAxis ("Horizontal")*moveSpeed;
		z = Input.GetAxis ("Vertical")*moveSpeed;
		if(moveType == MoveType.worldAxis)
		{
			transform.position = new Vector3 (transform.position.x+x, transform.position.y, transform.position.z+z);
		}
		else
		{
			transform.Rotate(transform.up,x*rotationSpeedOnLocalAxis);
			transform.Translate(0.0f,0.0f,z*100.0f*Time.deltaTime,transform);
		}

	}

	IEnumerator ChildManager ()
	{
		GameObject go = new GameObject("follower");
		go.transform.position = transform.position - new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1.0f);
		for(;;)
		{
			go.transform.position = new Vector3(Mathf.Lerp(go.transform.position.x,transform.position.x,moveSpeed*100.0f*Time.deltaTime),
				Mathf.Lerp(go.transform.position.y,transform.position.y,moveSpeed*100.0f*Time.deltaTime),
				Mathf.Lerp(go.transform.position.z,transform.position.z,moveSpeed*100.0f*Time.deltaTime));
			angle = Vector3.Angle(go.transform.position,transform.position);
			Debug.DrawLine(go.transform.position,transform.position);
			if(moveType == MoveType.worldAxis)
			{
				transform.LookAt(go.transform);
			}
			yield return null;
		}
	}
}