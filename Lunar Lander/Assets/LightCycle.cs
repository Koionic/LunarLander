using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    //The lightcycles transform
    Transform transform;

    //The transform to spawn the trail from
    [SerializeField]
    Transform backOfCycle;

    //velocity thats sends the lightcycle forward
    Vector3 move = Vector3.up;

    //multipler for the cycles velocity
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject trailPrefab;

    //The trail thats immediately behind the player
    GameObject currentTrail;

    //The position the player last turned
    Vector3 lastTurnPosition;

    bool sideways = false;
   
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void Start ()
    {
        //creates the intial trail at the start of the game
        lastTurnPosition = transform.position;
        currentTrail = Instantiate(trailPrefab, lastTurnPosition, Quaternion.identity);
	}
	
	void Update ()
    {
        //moves the player forward
        transform.Translate(move * speed);

        //extends the trail behind the player
        DrawTrail(false);

        //allows the player to turn left and right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Turn(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Turn(false);
        }
	}

    void DrawTrail(bool turning)
    {
        //moves the trail object to the midpoint between the last turn position and the cycle (the bracketed section changes the position slightly when turning so corners match up )
        currentTrail.transform.position = (lastTurnPosition + (turning ? transform.position : backOfCycle.position)) / 2;

        //changes the scale of the trail according to the players position
        currentTrail.transform.localScale = CalculateScale(turning);
    }

    Vector3 CalculateScale(bool turning)
    {
        //vector that gets returned at the end
        Vector3 vector;
        float scale;

        if (sideways)
        {
            //calculates the horizontal distance between the last turn position and the cycle
            scale = Mathf.Abs((turning ? transform.position.x : backOfCycle.position.x) - lastTurnPosition.x);
            vector = new Vector3(scale, .1f, 1);
        }
        else
        {
            //calculates the vertical distance between the last turn position and the cycle
            scale = Mathf.Abs((turning ? transform.position.y : backOfCycle.position.y) - lastTurnPosition.y);
            vector = new Vector3(.1f, scale, 1);
        }

        return vector;
    }

    //function that changes the players direction, finalises trail position and creates a new trail object
    void Turn(bool left)
    {
        //rotates the cycle
        transform.Rotate(0, 0, (left ? 90 : -90));

        //fixes up alignment of trails
        DrawTrail(true);

        //changes the last turning position to their current position
        lastTurnPosition = transform.position;

        //disassigns the current trail
        currentTrail = null;

        //creates a new trail object at the point the player turned
        currentTrail = Instantiate(trailPrefab, lastTurnPosition, Quaternion.identity);

        //toggles the bool on or off
        sideways = (sideways ? false : true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //destroys the player if it hits anything
        if (other.CompareTag("Wall") && other.gameObject != currentTrail)
        {
            Destroy(gameObject);
        }
    }
}
