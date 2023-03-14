using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;
using System.Security.Cryptography;

public class BowlingGameManager : MonoBehaviour
{
    public GameObject ball; 
    public GameObject pinSetPrefab;
    public float launchSpeed = 10f; 
    public float launchSpeedMultiplier;
    public GameObject arrowPrefab;
    public GameObject velocityPrefab;
    public GameObject velocityScalePrefab;
    public BallController ballController;
    public GameObject messageTextObject;
    public Transform bowlingBallSpawnPoint;
    public Transform pinSetSpawnPoint;
    public TMP_Text messageText;
    public GameObject restartButton, mainMenuButton;
    public ScoreManager scoreManager;
    public GameObject pinSet;

    public bool pinSpawned = false;
    public bool positionIsSelected = false;
    public bool velocityIsSelected = false;
    public bool launched = false; 
    
    public enum State { PreLaunch, PostLaunch};
    public State currentState = State.PreLaunch;
    public int turnCounter = 1;

    private void Start()
    {
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        PositionText();
        velocityPrefab.SetActive(false); arrowPrefab.SetActive(false); velocityScalePrefab.SetActive(false);
        Instantiate(pinSetPrefab, pinSetSpawnPoint);
        pinSet = GameObject.FindGameObjectWithTag("Pin Set");
    }
    void Update()
    {
        if(currentState == State.PreLaunch && turnCounter == 1)
        {

            if (Keyboard.current.eKey.wasPressedThisFrame && positionIsSelected == false && ballController.enabled == true)
            {
                SetBallPos();
            }

            //  Check if the F key is pressed + position is selected + allows user to choose velocity
            if (Keyboard.current.fKey.wasPressedThisFrame && positionIsSelected == true && ballController.enabled == false)
            {
                SetBallVelocity();
            }

            // Check if the space bar is pressed and the ball hasn't been launched yet
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !launched && positionIsSelected == true && ballController.enabled == false
                    && velocityIsSelected == true)
            {
                LaunchBall();
                StartCoroutine(RestartTextDelay());
                currentState = State.PostLaunch;
                
            }
        }

        if(currentState == State.PostLaunch && turnCounter == 1)
        {

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                Debug.Log("First turn R press");
                positionIsSelected = false;
                velocityIsSelected = false;
                launched = false;
                ball.GetComponent<Rigidbody>().isKinematic = true;
                ball.transform.SetPositionAndRotation(bowlingBallSpawnPoint.position, Quaternion.Euler(Vector3.zero));
                ballController.enabled = true;
                turnCounter = 2;
                currentState = State.PreLaunch;
                PositionText();
                Destroy(pinSet);
            }
        }

        if(currentState == State.PreLaunch && turnCounter == 2)
        {
            if(pinSpawned == false)
            {
                Instantiate(pinSetPrefab, pinSetSpawnPoint);

                pinSpawned = true;

                scoreManager.FindPins();

                scoreManager.strikeText.SetActive(false);
            }

            if (Keyboard.current.eKey.wasPressedThisFrame && positionIsSelected == false && ballController.enabled == true)
            {
                SetBallPos();
            }

            //  Check if the F key is pressed + position is selected + allows user to choose velocity
            if (Keyboard.current.fKey.wasPressedThisFrame && positionIsSelected == true && ballController.enabled == false)
            {
                SetBallVelocity();
            }

            // Check if the space bar is pressed and the ball hasn't been launched yet
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !launched && positionIsSelected == true && ballController.enabled == false
                    && velocityIsSelected == true)
            {
                LaunchBall();
                currentState = State.PostLaunch;
                StartCoroutine(GameOver());
            }
        }

    }
    private IEnumerator RestartTextDelay()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Waited for 5 seconds!");

        messageTextObject.SetActive(true);

        messageText.text = "Press 'R' to restart.";

    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Waited for 5 seconds!");
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
        messageTextObject.SetActive(true);
        messageText.text = "Game Over!";
    }


    void SetBallPos()
    {
        positionIsSelected = true;
        ballController.enabled = false;
        PowerText();
        velocityPrefab.SetActive(true);
        velocityScalePrefab.SetActive(true);
    }

    void SetBallVelocity()
    {
        launchSpeed = velocityPrefab.transform.position.y * launchSpeedMultiplier;
        velocityIsSelected = true;
        velocityPrefab.SetActive(false);
        velocityScalePrefab.SetActive(false);

        arrowPrefab.SetActive(true);
        LaunchText();
        ball.GetComponent<Rigidbody>().isKinematic = false;

    }

    void LaunchBall()
    {
        launched = true;
        messageTextObject.SetActive(false);
        // Get the current rotation of the arrow and convert it to a direction vector
        Vector3 arrowDirection = Quaternion.Euler(0f, arrowPrefab.transform.rotation.eulerAngles.y, 0f) * Vector3.forward;
        arrowPrefab.SetActive(false);
        // Launch the ball in the direction of the arrow
        ball.GetComponent<Rigidbody>().AddForce(arrowDirection * launchSpeed, ForceMode.Impulse);
    }
    
    void PositionText()
    {
        messageText.text = "Press the 'E' key to select the starting position";
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
