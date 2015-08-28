using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class ScriptBorgShip : MonoBehaviour {

    // Public Variables
    [Tooltip("Speed the borg ship rotates")]
    public float rotationSpeed = 100f;                          //Speed the ship rotates

    [Tooltip("Speed the borg ship moves")]
    public float moveSpeed = 0.1f;                              //Speed the ship moves

    [Tooltip("Game object representing the sun")]
    public GameObject sun;                                      //Sun representing the game object

    [Tooltip("Max distance the ship can go from the sun")]
    public float maxDistance = 1000f;                           //Maximum distance the ship can go from the sun.

    [Tooltip("Position that the ship will start, and respawn at")]
    public Vector3 spawnPosition;                               //Ship spawn and respawn location.

    [Tooltip("Textfield that the countdown will appear in when user dies")]
    public Text CountdownText;

    // Private Variables
    private bool canMove;

    private int count = 5;

	// Use this for initialization
	void Start ()
    {
        // Checks to make sure the variables are set to acceptable values in the editor.
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

	    if (sun == null)
	    {
	        Debug.Log("Sun game object is not set on the ship script.");
	    }
#endif
        DestroyShip();
	    CountdownText.text = "";
	    transform.position = spawnPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Getting the WASD inputs for rotating the ship.
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

        // Moves the ship forward at a constant pace if canMove is true.
	    if (canMove)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

        // Gets the distance the ship is from the sun.
	    float distance = Vector3.Distance(this.transform.position, sun.transform.position);

        // If that distance is greater than the maximum allowed distance, call the destroy ship function.
	    if (distance > maxDistance)
	    {
	        DestroyShip();
	    }
	}

    // Function that is called when the ship is destroyed.
    void DestroyShip()
    {
        canMove = false;
        transform.position = spawnPosition;
        InvokeRepeating("RespawnTimer", 1.0f, 1.0f);
    }

	void OnTriggerEnter(Collider colliderObj) // Craig
	{
		GameObject.Find("Sun").GetComponent<AudioSource>().Play();
		Debug.Log(("Ship hit planet " + colliderObj.name + " at " + this.transform.position.ToString()));
		DestroyShip();
	}

    void RespawnTimer()
    {
        CountdownText.text = count.ToString();
        count--;
        if (count < 0)
        {
            CountdownText.text = "";
            count = 5;
            canMove = true;
            CancelInvoke("RespawnTimer");
        }
    }
}
