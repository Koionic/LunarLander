using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    Rigidbody rb2d;

    Transform transform;

    [SerializeField] int playerID;

    [SerializeField] Transform spawnPoint;

    InputController inputController;

    AudioManager audioManager;

    float input;

    float fuel = 999f, score;

    [SerializeField] float fuelMultiplier;

    int totalCrashes;

    Collider landerCollider;

    [SerializeField] float rocketForce;

    [SerializeField] float landingThresholdRotation = 2f;
    [SerializeField] float landingThresholdMagnitude = 3f;

    [SerializeField] float velocityUIMultiplier = 5f;

    [SerializeField] float explosionRadius = 5.0F;
    [SerializeField] float explosionPower = 10.0F;

    [SerializeField] GameObject flame;
    [SerializeField] GameObject destroyedModel;

    [SerializeField] Text xMoveText, yMoveText, fuelText, scoreText;

    GameController gameController;

    [SerializeField] Animator cameraAnimator;

    [SerializeField] float closeTerrainRange, farTerrainRange;

    [SerializeField] bool zoomedIn1, zoomedIn2, outOfFuel;

    LandingZone landingZone;

    ZoneController zoneController;

    bool isDead = false, shielded, doublePoints;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody>();

        transform = GetComponent<Transform>();

        inputController = FindObjectOfType<InputController>();

        zoneController = FindObjectOfType<ZoneController>();

        landerCollider = GetComponent<Collider>();

        audioManager = AudioManager.instance;
    }

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            flame.SetActive(false);
        }
        else
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

            ManageCamera();

            //updates the ui
            if (xMoveText != null)
                xMoveText.text = "Horizontal Velocity: " + (int)(rb2d.velocity.x * velocityUIMultiplier);

            if (yMoveText != null)
                yMoveText.text = "Vertical Velocity: " + (int)(rb2d.velocity.y * -velocityUIMultiplier);

            if (fuelText != null)
            {
                fuelText.text = "Fuel Remaining: " + (fuel > 0 ? (int)fuel : 0);
            }

            if (scoreText != null)
                scoreText.text = "Score: " + score;

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
        {
            Debug.Log("Landed with a velocity of " + MyMagnitute(collision));
            AddScore();
            audioManager.PlaySound("SuccessfulLanding");
        }
        if (collision.gameObject.tag == "Terrain" && !Landed(collision))
            Crash(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case ("LZ"):
                landingZone = other.GetComponent<LandingZone>();
                return;

            case ("Fuel"):
                AddFuel();
                Destroy(other.gameObject);
                audioManager.PlaySound("PowerUp");
                return;

            case ("Shield"):
                EnableShield();
                Destroy(other.gameObject);
                audioManager.PlaySound("PowerUp");
                return;

            case ("DoublePoints"):
                EnableDoublePoints();
                Destroy(other.gameObject);
                audioManager.PlaySound("PowerUp");
                return;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LZ"))
        {
            landingZone = null;
        }
    }

    void AddScore()
    {
        float deltaScore = 50;

        if (landingZone != null)
        {
            deltaScore *= landingZone.GetMulti();
            zoneController.DeactivateSpawn(landingZone.gameObject);
            zoneController.AddZones(1);
            landingZone = null;
        }

        if (doublePoints)
        {
            deltaScore *= 2;
        }

        score += deltaScore;
    }

    void AddFuel()
    {
        fuel += 100;
    }

    void EnableShield()
    {
        shielded = true;
        Invoke("DisableShield", 10f);
    }

    void DisableShield()
    {
        shielded = false;
        CancelInvoke("DisableShield");
    }

    void EnableDoublePoints()
    {
        doublePoints = true;
        Invoke("DisableDoublePoints", 10f);
    }

    void DisableDoublePoints()
    {
        doublePoints = false;
        CancelInvoke("DisableDoublePoints");
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

    bool IsNearingTerrain(bool close)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, close ? closeTerrainRange : farTerrainRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Terrain"))
            {
                return true;
            }
        }
        return false;
    }

    void ManageCamera()
    {
        if (IsNearingTerrain(true) && !zoomedIn2)
        {
            cameraAnimator.Play("Med-Close");
            zoomedIn2 = true;
        }
        else if (!IsNearingTerrain(true) && zoomedIn2)
        {
            cameraAnimator.Play("Close-Med");
            zoomedIn2 = false;
        }
        else if (IsNearingTerrain(false) && !zoomedIn1)
        {
            cameraAnimator.Play("Far-Med");
            zoomedIn1 = true;
        }
        else if (!IsNearingTerrain(false) && zoomedIn1)
        {
            cameraAnimator.Play("Med-Far");
            zoomedIn1 = false;
        }
    }

    void Crash(Collision collision)
    {
        if (shielded)
        {
            DisableShield();
        }
        else
        {
            Instantiate(destroyedModel, transform.position, transform.rotation);
            Explosion(collision);
            isDead = true;
            totalCrashes++;

            gameController.InvokeRespawn(this);

            gameObject.SetActive(false);
        }
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

    public bool IsOutOfFuel()
    {
        return outOfFuel;
    }
}
