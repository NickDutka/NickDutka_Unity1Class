using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Runner : MonoBehaviour
{

    public float moveChance = 0.25f;
   
    
    GameManager gameManager;
    public GameObject gameManagerObject;

    private void Awake()
    {

        gameManagerObject = GameObject.Find("Brain");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MuddyTerrain"))
        {
            Debug.Log("onMuddy");
            moveChance = 0.125f;
        }
        else if (other.gameObject.CompareTag("RegularTerrain"))
        {
            Debug.Log("onGrass");
            moveChance = 0.25f;
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {

            gameManager.Stop();
            PrintWinner();
            
            

        }
    }

    public void PrintWinner()
    {
        
        
        Debug.Log("Game Over");

        Debug.Log(name + " is the winner");

        gameManager.winnerText.text = name + " is the winner!";



    }


    public void Move()
    {

        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= moveChance)
        {
            transform.position += new Vector3(0, 0, 10);
        }

    }

    //private void GameOver()
    //{
    //    Debug.Log("Game Over");

    //    Debug.Log(name + " is the winner");



    //    if (playersInGame == null)
    //        playersInGame = GameObject.FindGameObjectsWithTag("Player");

    //    foreach (GameObject go in playersInGame)
    //    {
    //        go.GetComponent<Runner>().Stop();
    //    }
    //}

    //public void Stop()
    //{
    //    StopAllCoroutines();
    //}
    //private void Start()
    //{


    //    StartCoroutine(GameUpdate());
    //}

    //public IEnumerator GameUpdate()
    //{
    //    while (true)
    //    {


    //        yield return new WaitForSeconds(turnDuration);
    //        Move();

    //    }
    //}


}

    

