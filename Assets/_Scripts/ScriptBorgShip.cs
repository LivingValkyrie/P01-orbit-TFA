using UnityEngine;
using System.Collections;

public class ScriptBorgShip : MonoBehaviour {

    [Tooltip("Speed the borg ship rotates")]
    public float rotationSpeed = 1.5f;

    [Tooltip("Speed the borg ship moves")]
    public float moveSpeed = 5.0f;

    [Tooltip("Game object representing the sun")]
    public GameObject sun;

    [Tooltip("Max distance the ship can go from the sun")]
    public float maxDistance = 10000f;

	// Use this for initialization
	void Start ()
    {
#if UNITY_EDITOR
        if (rotationSpeed == 0)
        {
            Debug.Log(("Rotation speed is set to zero, ship will not turn."));
        }
            
        if (moveSpeed == 0)
        {
            Debug.Log(("Move speed is set to zero, ship will not move."));
        }

	    if (maxDistance == 0)
	    {
	        Debug.Log("Max distance ship can go from sun is zero, game will be unplayable.");
	    }
#endif
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -(Time.deltaTime * rotationSpeed));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.right, -(Time.deltaTime * rotationSpeed));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);
        }

        transform.Translate(Vector3.forward * moveSpeed, Space.Self);

	    float distance = Vector3.Distance(this.transform.position, sun.transform.position);
        Debug.Log(distance);

	    if (distance > maxDistance)
	    {
	        DestroyShip();
	    }
	}

    void DestroyShip()
    {
        Debug.Log(("Ship destroyed"));
        Destroy(this.gameObject);
    }
}
