using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float rotationSpeed = 100f; // speed of arrow rotation
    public float maxAngle = 45f; // maximum angle of rotation in either direction
    public bool rotateRight = true; // whether the arrow is currently rotating right or left
    

    void Update()
    {
        // Rotate the arrow back and forth between -45 and 45 degrees
        if (rotateRight)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.y >= maxAngle && transform.rotation.eulerAngles.y <= 180)
            {
                rotateRight = false;
            }
        }
        else
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.y <= 360 - maxAngle && transform.rotation.eulerAngles.y >= 180)
            {
                rotateRight = true;
            }
        }
    }
}






