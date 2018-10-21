using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    float teleportingOffset = 2;

    private void OnTriggerEnter(Collider collision)
    {
        Vector3 newPos = collision.gameObject.transform.position;


        if (collision.gameObject.transform.position.x < 0)
        {
            newPos.x = -(collision.gameObject.transform.position.x + teleportingOffset);
        }

        if (collision.gameObject.transform.position.x > 0)
        {
            newPos.x = -(collision.gameObject.transform.position.x - teleportingOffset);
        }

        collision.GetComponent<Rigidbody>().MovePosition(newPos);
    }
}
