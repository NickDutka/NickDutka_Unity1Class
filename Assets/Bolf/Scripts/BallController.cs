using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float strafeSpeed = 1f; // speed of strafing movement
    public float strafeDistance = 2f; // distance of strafing movement

    private Vector3 startingPos; // starting position of the ball
    private bool strafingRight = true; // whether the ball is currently strafing right

    void Start()
    {
        startingPos = transform.position; // save the starting position of the ball
    }

    void Update()
    {
        // Calculate the target position based on the current strafing direction
        Vector3 targetPos;
        if (strafingRight)
        {
            targetPos = new Vector3(startingPos.x + strafeDistance, transform.position.y, transform.position.z);
        }
        else
        {
            targetPos = new Vector3(startingPos.x - strafeDistance, transform.position.y, transform.position.z);
        }

        // Move the ball towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, strafeSpeed * Time.deltaTime);

        // Check if the ball has reached the target position and update the strafing direction
        if (transform.position.x >= startingPos.x + strafeDistance)
        {
            strafingRight = false;
        }
        else if (transform.position.x <= startingPos.x - strafeDistance)
        {
            strafingRight = true;
        }
    }
}
