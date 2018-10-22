using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandingZone : MonoBehaviour {

    [SerializeField] int multiplier;

    [SerializeField] TextMeshProUGUI multiText;

    [SerializeField] float moveSpeed;

    [SerializeField] bool down = true;

    [SerializeField] float deltaMove;

    Vector3 center;

	// Use this for initialization
	void OnEnable()
    {
        if (multiText != null)
        multiText.text = "X" + multiplier;
	}

    private void Start()
    {
        center = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        deltaMove = center.y - transform.position.y;

        if (deltaMove <= 0)
            down = true;
        if (deltaMove >= 0.5f)
            down = false;

        Vector3 vector3 = new Vector3(0, down ? -1 : 1, 0);

        transform.Translate(vector3 * moveSpeed);
	}

    public int GetMulti()
    {
        return multiplier;
    }
}
