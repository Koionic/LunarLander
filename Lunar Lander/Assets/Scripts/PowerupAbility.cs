using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAbility : MonoBehaviour
{

    [Header("Deletes Powerup after a couple seconds")]

    [SerializeField]
    float lifeTime;

	void Start ()
    {

        Destroy(gameObject, lifeTime);

	}

}
