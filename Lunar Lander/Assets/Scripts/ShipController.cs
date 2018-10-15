﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    Rigidbody rb2d;

    Transform transform;

    [SerializeField] int playerID;

    float input;

    [SerializeField] float rocketForce;

    [SerializeField] float landingThresholdRotation = 2f;
    [SerializeField] float landingThresholdMagnitude = 3f;

    [SerializeField] float velocityUIMultiplier = 5f;

    [SerializeField] float explosionRadius = 5.0F;
    [SerializeField] float explosionPower = 10.0F;

    [SerializeField] GameObject flame;
    [SerializeField] GameObject destroyedModel;

    [SerializeField] Text xMoveText, yMoveText;

    public static DeviceType deviceType;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody>();

        transform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {
        deviceType = SystemInfo.deviceType;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /* //moves the camera to follow the player (will add static camera movement for mulitplayer)
         mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20); */

<<<<<<< HEAD
        if (SceneManager.GetActiveScene().name == "Lunar Lander Blake")
=======
        //grabs the horizontal inputs from the joystick/keyboard
        input = Input.GetAxis("Horizontal" + playerID);

        //rotates the ship around its z axis
        rb2d.angularVelocity = new Vector3(0, 0, -input);

        //updates the ui
      //  xMoveText.text = "Horizontal Velocity: " + (int)(rb2d.velocity.x * velocityUIMultiplier);
       // yMoveText.text = "Vertical Velocity: " + (int)(rb2d.velocity.y * -velocityUIMultiplier);

        //boosts the rocket in the direction it is facing
        if (Input.GetAxis("Throttle" + playerID) > 0f)
        {
            rb2d.AddForce(transform.up * rocketForce * Input.GetAxis("Throttle" + playerID));

            if(flame != null)
            flame.SetActive(true);
        }
        else
>>>>>>> c6e8a04ff912d1bdb27678fdde0d61eb9603883f
        {
            //grabs the horizontal inputs from the joystick/keyboard
            input = Input.GetAxis("Horizontal" + playerID);

            //rotates the ship around its z axis
            rb2d.angularVelocity = new Vector3(0, 0, -input);

            //updates the ui
            xMoveText.text = "Horizontal Velocity: " + (int)(rb2d.velocity.x * velocityUIMultiplier);
            yMoveText.text = "Vertical Velocity: " + (int)(rb2d.velocity.y * -velocityUIMultiplier);

            //boosts the rocket in the direction it is facing
            if (Input.GetAxis("Throttle" + playerID) > 0f)
            {
                rb2d.AddForce(transform.up * rocketForce * Input.GetAxis("Throttle" + playerID));

                if (flame != null)
                    flame.SetActive(true);
            }
            else
            {
                if (flame != null)
                    flame.SetActive(false);
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" && Landed(collision))
            Debug.Log("Landed with a velocity of " + MyMagnitute(collision));
        if (collision.gameObject.tag == "Terrain" && !Landed(collision))
            Crash(collision);
    }

    bool Landed(Collision collision)
    {
        if (transform.rotation.z <= landingThresholdRotation && transform.rotation.z >= -landingThresholdRotation && MyMagnitute(collision) <= landingThresholdMagnitude)
            return true;
        else
            return false;
    }

    float MyMagnitute(Collision collision)
    {
        return (collision.relativeVelocity.magnitude * velocityUIMultiplier);
    }

    void Crash(Collision collision)
    {
        Instantiate(destroyedModel, transform.position, transform.rotation);
        Explosion(collision);
        Destroy(gameObject);
    }

    void Explosion(Collision collision)
    {
        Vector3 explosionPos = collision.contacts[0].point;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionPower * collision.relativeVelocity.magnitude, explosionPos, explosionRadius);
        }
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public void SetPlayerID(int id)
    {
        playerID = id;
    }
}
