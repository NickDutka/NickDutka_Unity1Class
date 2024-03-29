using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KnockedOver : MonoBehaviour
{
    public float zRotation;
    public float rotationDifference;
    public int score = 0;
    public bool alreadyCountedScore = false;
    public bool counted = false;
    public TMP_Text scoreText;
    public bool hasFallen = false;
    public float rotationDifferenceFromUp;
    private float initialZRotation;

    private void Start()
    {
        initialZRotation = transform.eulerAngles.z;
    }

    private void Update()
    {
        // Set hasFallen to true or false based on whether the pin
        // has "fallen".

        zRotation = transform.eulerAngles.z;
        rotationDifference = zRotation - initialZRotation;

        if (rotationDifference > 180)
            rotationDifference = 360 - rotationDifference;

        if (rotationDifference > 45)
        {
            // hasFallen = true;
        }

        Debug.DrawRay(transform.position, Vector3.up * 4, Color.blue);
        Debug.DrawRay(transform.position, transform.up * 4, Color.blue);

        rotationDifferenceFromUp = Vector3.Angle(Vector3.up, transform.up);

        if (rotationDifferenceFromUp > 45)
        {
            hasFallen = true;
            
        }

        if (hasFallen && alreadyCountedScore == false)
        {
            score = 1;
            
            alreadyCountedScore = true;
        } 
    }
}
