using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    Rigidbody rb2d;

    Transform transform;

    [SerializeField] int playerID;

    [SerializeField] Transform spawnPoint;

    InputController inputController;

    float input;

    float fuel = 999f;

    [SerializeField] float fuelMultiplier;

    int totalCrashes;

    [SerializeField] Renderer landerRenderer;

    Collider landerCollider;

    [SerializeField] float rocketForce;

    [SerializeField] float landingThresholdRotation = 2f;
    [SerializeField] float landingThresholdMagnitude = 3f;

    [SerializeField] float velocityUIMultiplier = 5f;

    [SerializeField] float explosionRadius = 5.0F;
    [SerializeField] float explosionPower = 10.0F;

    [SerializeField] GameObject flame;
    [SerializeField] GameObject destroyedModel;

    [SerializeField] Text xMoveText, yMoveText;

    GameController gameController;

    bool isDead = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody>();

        transform = GetComponent<Transform>();

        inputController = FindObjectOfType<InputController>();

        landerCollider = GetComponent<Collider>();
    }

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            gameController = FindObjectOfType<GameController>();

            GetJoystickAssignments();

            if(playerID == 0)
            {
                gameObject.SetActive(false);
            }
        }
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        /* //moves the camera to follow the player (will add static camera movement for mulitplayer)
         mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20); */

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            
        }
        else
        {
            //grabs the horizontal inputs from the joystick/keyboard
            input = inputController.GetHorizontalInput(playerID);

            //rotates the ship around its z axis
            rb2d.angularVelocity = new Vector3(0, 0, -input);

            //updates the ui
            if (xMoveText != null)
                xMoveText.text = "Horizontal Velocity: " + (int)(rb2d.velocity.x * velocityUIMultiplier);

            if (yMoveText != null)
                yMoveText.text = "Vertical Velocity: " + (int)(rb2d.velocity.y * -velocityUIMultiplier);

            //boosts the rocket in the direction it is facing
            if (inputController.GetThrottleInput(playerID) > 0f && fuel > 0)
            {
                float thruttleForce = rocketForce * inputController.GetThrottleInput(playerID);
                rb2d.AddForce(transform.up * thruttleForce);

                fuel -= thruttleForce * Time.deltaTime * fuelMultiplier;

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
        isDead = true;
        totalCrashes++;

        gameController.InvokeRespawn(this);

        gameObject.SetActive(false);
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

    void GetJoystickAssignments()
    {
        PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();

        playerID = playerInfo.GetJoystick(playerID - 1);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Revive()
    {
        isDead = false;
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
