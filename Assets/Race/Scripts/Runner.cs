using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    TurnCounter turncounter;
    public GameObject turnCounter;
    public float moveChance = 0.25f;
    public float turnDuration = 3f;


    private void Awake()
    {
        turncounter = turnCounter.GetComponent<TurnCounter>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MuddyTerrain")
        {
            moveChance = 0.125f;
        }
        else if (other.gameObject.tag == "RegularTerrain")
        {
            moveChance = 0.25f;
        }
    }

    public IEnumerator Move()
    {
        while (true)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= moveChance)
            {
                transform.position += new Vector3(0, 0, 10);
            }
            yield return new WaitForSeconds(turnDuration);
            turncounter.IncreaseTurn();
        }
    }

    private void Start()
    {
        StartCoroutine(Move());
    }
}

    

