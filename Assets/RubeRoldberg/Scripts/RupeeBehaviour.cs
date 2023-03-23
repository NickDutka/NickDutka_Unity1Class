using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeBehaviour : MonoBehaviour
{
    public float minHeight = 1f; // minimum height of the object
    public float maxHeight = 2f; // maximum height of the object
    public float speed = 0.5f; // speed of the object's up/down movement
    public float rupeeRotationSpeed = 1f;

    private bool movingUp = true; // flag to track if the object is currently moving up or down

    void Update()
    {
        // Calculate the new position based on the current position and the direction and speed of movement
        float newYPos = transform.position.y + (movingUp ? 1f : -1f) * speed * Time.deltaTime;

        // If the new position is outside the height range, reverse the direction of movement
        if (newYPos < minHeight || newYPos > maxHeight)
        {
            movingUp = !movingUp;
            newYPos = Mathf.Clamp(newYPos, minHeight, maxHeight);
        }

        // Set the new position
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);

        transform.Rotate(Vector3.up, rupeeRotationSpeed * Time.deltaTime);
    }
}
