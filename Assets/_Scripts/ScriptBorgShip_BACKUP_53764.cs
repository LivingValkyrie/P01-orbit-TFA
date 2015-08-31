using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class ScriptBorgShip : MonoBehaviour {

    // Public Variables
    [Tooltip("Speed the borg ship rotates")]
    public float rotationSpeed = 100f; //Speed the ship rotates

    [Tooltip("Speed the borg ship moves")]
    public float moveSpeed = 1.0f; //Speed the ship moves

    [Tooltip("Game object representing the sun")]
    public GameObject sun; //Sun representing the game object

    [Tooltip("Max distance the ship can go from the sun")]
    public float maxDistance = 1000f; //Maximum distance the ship can go from the sun.

    [Tooltip("Position that the ship will start, and respawn at")]
    public Vector3 spawnPosition; //Ship spawn and respawn location.

    [Tooltip("Time for ship to respawn.")]
    public int respawnTime;

    [Tooltip("Textfield that the countdown will appear in when user restarts")]
    public Text CountdownText;

    [Tooltip("Textfield that will appear explaining what to do when player dies.")]
    public Text DeathText;

    [Tooltip("Textfield that will show how many times the player has died")]
    public Text deathCountText;

    [Tooltip("The explosion animation to play when you crash")]
    public GameObject boomAnimation;

    // Private Variables
    bool canMove = true;
    bool isDead = false;
    int count;

    // Use this for initialization
    void Start() {
        // Checks to make sure the variables are set to acceptable values in the editor.
        // @author: Nathan
#if UNITY_EDITOR
        if (rotationSpeed == 0) {
            Debug.Log(("Rotation speed is set to zero, ship will not turn."));
        }

        if (moveSpeed == 0) {
            Debug.Log(("Move speed is set to zero, ship will not move."));
        }

        if (maxDistance == 0) {
            Debug.Log("Max distance ship can go from sun is zero, game will be unplayable.");
        }

        if (sun == null) {
            Debug.Log("Sun game object is not set on the ship script.");
        }

        if (DeathText == null) {
            Debug.Log("DeathText is not set in ship script.");
        }

        if (CountdownText == null) {
            Debug.Log("CountdownText is not set in ship script.");
        }

        //gipson 

        if (boomAnimation == null) {
            Debug.Log("boomAnimation is not set in ship script.");
        }

        if (deathCountText == null) {
            Debug.Log("deathCountText is not set in ship script.");
        }
#endif
        CountdownText.text = "";
        DeathText.text = "";
        count = respawnTime;
        transform.position = spawnPosition;

        //save state code - gipson
        if (!PlayerPrefs.HasKey("HasRun")) {
            PlayerPrefs.SetString("HasRun", "Yes");
            PlayerPrefs.SetInt("Deaths", 0);
        }
    }

    // Update is called once per frame
    // @author: Nathan
    void Update() {
        //gipson death count
        deathCountText.text = "Lifetime Deaths: " + PlayerPrefs.GetInt("Deaths");

        // Moves the ship forward at a constant pace if canMove is true.
        if (canMove) {
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime, Space.Self);

            // Getting the WASD inputs for rotating the ship.
            if (Input.GetKey(KeyCode.A)) {
                transform.Rotate(Vector3.up, -(Time.deltaTime*rotationSpeed));
            }
            else if (Input.GetKey(KeyCode.D)) {
                transform.Rotate(Vector3.up, Time.deltaTime*rotationSpeed);
            }
            if (Input.GetKey(KeyCode.W)) {
                transform.Rotate(Vector3.right, -(Time.deltaTime*rotationSpeed));
            }
            else if (Input.GetKey(KeyCode.S)) {
                transform.Rotate(Vector3.right, Time.deltaTime*rotationSpeed);
            }
        }

        if (isDead) {
            if (Input.GetKey(KeyCode.Escape)) {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Space)) { //changed to GetKeyDown by gipson
                DeathText.text = "";
                InvokeRepeating("RespawnTimer", 0f, 1.0f);
            }
        }

        // Gets the distance the ship is from the sun.
        float distance = Vector3.Distance(this.transform.position, sun.transform.position);

        // If that distance is greater than the maximum allowed distance, call the destroy ship function.
        if (distance > maxDistance) {
            DestroyShip();
        }
    }

    // Function that is called when the ship is destroyed.
    // @author: Nathan
    void DestroyShip() {
        DeathText.text = "Press Esc to exit game\nPress spacebar to start again\nSpam to speed up respawn"; //gipson modified for feature
        canMove = false;
        Instantiate(boomAnimation, transform.position, transform.rotation); //gipson animation spawner
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1); //gipson death counter
        isDead = true;
        transform.position = spawnPosition;
    }

    void OnTriggerEnter(Collider colliderObj) // Craig
    {
        GameObject.Find("Sun").GetComponent<AudioSource>().Play();
        Debug.Log(("Ship hit planet " + colliderObj.name + " at " + this.transform.position.ToString()));
        DestroyShip();
    }

    // Function that decrements the time in countdown.
    // @author: Nathan
    void RespawnTimer() {
        CountdownText.text = count.ToString();
        count--;
        if (count < 0) {
            CountdownText.text = "";
            count = respawnTime;
            canMove = true;
            isDead = false;
            CancelInvoke("RespawnTimer");
        }
    }
}