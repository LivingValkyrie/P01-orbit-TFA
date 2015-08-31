using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: SateliteObject 
/// </summary>
public class SateliteObject : MonoBehaviour {
    #region Fields

    [Tooltip("The object that this object revolves around.")]
    public Transform parentBody;

    [Tooltip("The speed at which this object rotates")]
    public float rotationSpeed;

    [Tooltip("The speed at which this object revolves around its parentBody")]
    public float revolutionSpeed;

    #endregion

    void Start() {
#if UNITY_EDITOR

        if (parentBody == null) {
            Debug.Log("parent body has not been set for " + transform.name);
        }

        if (rotationSpeed == 0) {
            Debug.Log("rotation speed has not been set for " + transform.name);
        }

        if (revolutionSpeed == 0) {
            Debug.Log("revolution speed has not been set for " + transform.name);
        }

#endif
    }

    void Update() {
        transform.RotateAround(parentBody.position, Vector3.up, (revolutionSpeed*Time.deltaTime*1000)/365);
        transform.Rotate(Vector3.up, Time.deltaTime*rotationSpeed);
    }

}