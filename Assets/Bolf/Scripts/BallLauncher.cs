using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody ball; // reference to the bowling ball
    public float launchSpeed = 10f; // speed at which the ball is launched
    public float launchSpeedMultiplier;
    public GameObject arrowPrefab;
    public GameObject velocityPrefab;
    public GameObject velocityScalePrefab;
    public BallController ballController;

    public TMP_Text messageText;


    public bool positionIsSelected = false;
    public bool velocityIsSelected = false;

    private bool launched = false; // whether the ball has already been launched

    private void Start()
    {
        messageText.text = "Press the 'E' key to select the starting position";

        velocityPrefab.SetActive(false); arrowPrefab.SetActive(false); velocityScalePrefab.SetActive(false);
    }
    void Update()
    {
        // Check if E key is pressed to set the ball position.
        if (Keyboard.current.eKey.wasPressedThisFrame && positionIsSelected == false && ballController.enabled == true)
        {
            positionIsSelected = true;
            ballController.enabled = false;
            PowerText();
            velocityPrefab.SetActive(true);
            velocityScalePrefab.SetActive(true);
            
        }

        //  Check if the F key is pressed + position is selected + allows user to choose velocity
        if (Keyboard.current.fKey.wasPressedThisFrame && positionIsSelected == true && ballController.enabled == false)
        {
            SetBallVelocity();
            arrowPrefab.SetActive(true);
            LaunchText();
            ball.isKinematic = false;
        }

        // Check if the space bar is pressed and the ball hasn't been launched yet
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !launched && positionIsSelected == true && ballController.enabled == false
                && velocityIsSelected == true)
        {
            
            launched = true;
            LaunchBall();
            
        }
    }

    void SetBallVelocity()
    {
        launchSpeed = velocityPrefab.transform.position.y * launchSpeedMultiplier;
        velocityIsSelected = true;
        Destroy(velocityPrefab);
        Destroy(velocityScalePrefab);
    }

    void LaunchBall()
    {
        // Get the current rotation of the arrow and convert it to a direction vector
        Vector3 arrowDirection = Quaternion.Euler(0f, arrowPrefab.transform.rotation.eulerAngles.y, 0f) * Vector3.forward;

        Destroy(arrowPrefab);

        // Launch the ball in the direction of the arrow
        ball.AddForce(arrowDirection * launchSpeed, ForceMode.Impulse);
    }

    void LaunchText()
    {
        messageText.text = "Press the 'Space' key to launch!";
    }
    void PowerText()
    {
        messageText.text = "Press the 'F' key to set the power.";
    }
}
