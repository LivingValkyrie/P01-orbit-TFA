using UnityEngine;
using System.Collections;

public class ScriptBorgShip : MonoBehaviour {

    [Tooltip("Speed the borg ship rotates")]
    public float rotationSpeed = 1.5f;

    [Tooltip("Speed the borg ship moves")]
    public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, -(Time.deltaTime * rotationSpeed));
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right, -(Time.deltaTime * rotationSpeed));
        }

        transform.Translate(Vector3.forward * moveSpeed, Space.Self);
	}
}
