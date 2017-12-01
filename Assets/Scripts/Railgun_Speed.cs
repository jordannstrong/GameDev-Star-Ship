using UnityEngine;
using System.Collections;

public class Railgun_Speed : MonoBehaviour
{
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
