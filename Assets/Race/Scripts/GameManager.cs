using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    
    public float turnDuration = 0.1f;

    private int turnCount = 0;

    public TMP_Text winnerText;
    public TMP_Text turnText;
    public Button startButton;

    private void Start()
    {
        
    }
    public void StartGame()
    {
        StartCoroutine(GameUpdate());
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit rayHit;
            Ray rayOrigin = cam.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue());

            if(Physics.Raycast(rayOrigin, out rayHit))
            {
                

                if (rayHit.transform.CompareTag("Player"))
                {
                    Transform goHit = rayHit.transform;
                    
                    Debug.Log(goHit.name);
                    
                    goHit.tag = "SelectedPlayer";

                    GameObject playerone = GameObject.FindGameObjectWithTag("SelectedPlayer");

                    playerone.GetComponent<Renderer>().material.color = Color.red;

                    playerone.name = "Player 1";

                    cam.transform.SetParent(playerone.transform);
                    //cam.transform.position = playerone.transform.position + new Vector3(15, 4.5f, 0);
                    //cam.transform.rotation = Quaternion.Euler(20, 270, 0);
                    cam.transform.position = playerone.transform.position + new Vector3(0, 4.5f, -12.5f);
                    cam.transform.rotation = Quaternion.Euler(20, 0, 0);

                    startButton.gameObject.SetActive(true);
                }
                
                
            }
        }
    }

    public IEnumerator GameUpdate()
    {
        while (true)
        {

            GameObject[] aiPlayers = GameObject.FindGameObjectsWithTag("Player");
            GameObject playerCharacter = GameObject.FindGameObjectWithTag("SelectedPlayer");

            playerCharacter.GetComponent<Runner>().Move();
            for (int i = 0; i < aiPlayers.Length; i++)
            {
                
                aiPlayers[i].GetComponent<Runner>().Move();
            }

            
            
         
            yield return new WaitForSeconds(turnDuration);
            turnCount ++;
            turnText.text = "Turn Counter: " + turnCount.ToString();
           
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
