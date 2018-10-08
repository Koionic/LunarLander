using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{

    Transform transform;

    Vector3 move = new Vector3(0, 1);

    [SerializeField]
    float speed;

    Vector3 lastTurnPosition;
   
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {
        lastTurnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(move * speed);

        Debug.DrawLine(transform.position, lastTurnPosition, Color.yellow, 20f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Turn(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Turn(false);
        }
	}

    void Turn(bool left)
    {

        if (move == Vector3.up)
        {
            move = (left ? Vector3.left : Vector3.right);
        }
        else if (move == Vector3.down)
        {
            move = (left ? Vector3.right : Vector3.left);
        }
        else if (move == Vector3.left)
        {
            move = (left ? Vector3.down : Vector3.up);
        }
        else if (move == Vector3.right)
        {
            move = (left ? Vector3.up : Vector3.down);
        }

        lastTurnPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        Destroy(gameObject);
    }
}
