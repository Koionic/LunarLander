using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    float teleportingOffset = 2;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newPos = collision.gameObject.transform.position;
        if (collision.gameObject.transform.position.x < 0)
            newPos.x = -(collision.gameObject.transform.position.x + teleportingOffset);
        if (collision.gameObject.transform.position.x > 0)
            newPos.x = -(collision.gameObject.transform.position.x - teleportingOffset);
        collision.gameObject.transform.position = newPos;
    }
}
