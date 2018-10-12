using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Text xMoveText, yMoveText;

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
        //moves the camera to follow the player (will add static camera movement for mulitplayer)
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20);

        //grabs the horizontal inputs from the joystick/keyboard
        input = Input.GetAxis("Horizontal");

        //rotates the ship around its z axis
        rb2d.angularVelocity = new Vector3(0, 0, -input);

        //updates the ui
        xMoveText.text = "Horizontal Velocity: " + (int)(rb2d.velocity.x * 5);
        yMoveText.text = "Vertical Velocity: " + (int)(rb2d.velocity.y * -5);

        //boosts the rocket in the direction it is facing
        if (Input.GetButton("Jump"))
        {
            Debug.Log("fire");
            rb2d.AddForce(transform.up * rocketForce);

            if(flame != null)
            flame.SetActive(true);
        }
        else
        {
            if (flame != null)
            flame.SetActive(false);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
