using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabLocomotion : MonoBehaviour
{
    public Transform right;
    public float moveSpeed = 1;

    private MyVrInputController input;

    private Vector3 previousPosition;

    private bool isMoving;

    private void Awake()
    {
        input = GetComponent<MyVrInputController>();

        previousPosition = right.position;
    }

    private void Update()
    {
        if (isMoving == true)
        {
            Vector3 displacement = previousPosition - right.position;

            transform.position += displacement * moveSpeed;

            if (input.RightTrigger < 0.5f)
            {
                isMoving = false;
            }
        }
        else
        {
            if (input.RightTrigger > 0.7f)
            {
                isMoving = true;
            }
        }

        previousPosition = right.position;
    }
}
