using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{

    Transform transform;

    [SerializeField]
    Transform backOfCycle;

    Vector3 move = Vector3.up;

    [SerializeField]
    float speed;

    [SerializeField]
    GameObject trailPrefab;

    GameObject trail;

    Vector3 lastTurnPosition;

    bool sideways = false;
   
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {
        lastTurnPosition = transform.position;
        trail = Instantiate(trailPrefab, lastTurnPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
    {
        move = Vector3.up;

        transform.Translate(move * speed);

        DrawTrail(false);

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
        trail.transform.position = (lastTurnPosition + (turning ? transform.position : backOfCycle.position)) / 2;

        trail.transform.localScale = CalculateScale(turning);
    }

    Vector3 CalculateScale(bool turning)
    {
        Debug.Log(transform.rotation.z);

        Vector3 vector;
        float scale;

        if (sideways)
        {
            scale = Mathf.Abs((turning ? transform.position.x : backOfCycle.position.x) - lastTurnPosition.x);
            vector = new Vector3(scale, .1f, 1);
        }
        else
        {
            scale = Mathf.Abs((turning ? transform.position.y : backOfCycle.position.y) - lastTurnPosition.y);
            vector = new Vector3(.1f, scale, 1);
        }

        return vector;
    }

    void Turn(bool left)
    {
        transform.Rotate(0, 0, (left ? 90 : -90));

        DrawTrail(true);

        lastTurnPosition = transform.position;

        trail = null;

        trail = Instantiate(trailPrefab, lastTurnPosition, Quaternion.identity);

        sideways = (sideways ? false : true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && other.gameObject != trail)
        Destroy(gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("meh");
    }
}
