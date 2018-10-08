using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    Rigidbody rb2d;

    Transform transform;

    float input;

    [SerializeField]
    float rocketForce;

    [SerializeField]
    GameObject flame;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody>();

        transform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        input = Input.GetAxis("Horizontal");

        rb2d.angularVelocity = new Vector3(0, 0, -input);

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("fire");
            rb2d.AddForce(transform.up * rocketForce);
            flame.SetActive(true);
        }
        else
        {
            flame.SetActive(false);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
