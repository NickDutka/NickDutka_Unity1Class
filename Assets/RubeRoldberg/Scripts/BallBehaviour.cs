using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BallBehaviour : MonoBehaviour
{
    public bool allRupeesCollected = false;
    public bool greenRupeesCollected = false;
    public bool blueRupeesCollected = false;
    public bool hasWon = false;

    public TMP_Text rupeeCountText;

    public AudioSource rigAudio;
    public AudioSource backgroundMusic;
    public AudioClip rupeeSound;
    public AudioClip secretSound;
    public AudioClip winSound;

    public GameObject[] blueRupees;
    public GameObject[] redRupees;
    public GameObject gate;
    public GameObject explosionPrefab;
    public GameObject[] winEffects;
    public GameObject winAreaObject;
    public GameObject winPanel;
    public GameObject infoPanel;

    public Transform ballRespawnPoint;

    public int rupeeCount;



    // Start is called before the first frame update
    void Start()
    {
        rupeeCount = 0;

        winAreaObject.SetActive(false);

        foreach (var bluerupee in blueRupees)
        {
            bluerupee.gameObject.SetActive(false);
        }
        foreach (var redRupee in redRupees)
        {
            redRupee.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rupeeCount >= 5)
        {
            greenRupeesCollected = true;
            
        }
        if(greenRupeesCollected == true)
        {
            rupeeCountText.color = Color.cyan;
            foreach (var bluerupee in blueRupees)
            {
                if(bluerupee != null)
                {
                    bluerupee.gameObject.SetActive(true);
                    greenRupeesCollected = false;
                }
            }
        }

        if (rupeeCount >=8)
        {
            blueRupeesCollected = true;
        }
        if (blueRupeesCollected == true)
        {
            rupeeCountText.color = Color.red;
            foreach (var redRupee in redRupees)
            {
                if(redRupee != null)
                {
                    redRupee.gameObject.SetActive(true);
                    blueRupeesCollected = false;
                }
            }
        }

        if (rupeeCount == 10 && !hasWon)
        {
            hasWon = true;
            allRupeesCollected = true;
            Debug.Log("All rupees");
            rigAudio.PlayOneShot(secretSound);
            explosionPrefab.SetActive(true);
            Destroy(gate);
            winAreaObject.SetActive(true);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rupee"))
        {
            rupeeCount++;
            Debug.Log(rupeeCount);
            rupeeCountText.text = "Rupees: " + rupeeCount;
            rigAudio.PlayOneShot(rupeeSound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("BallReset"))
        {
            gameObject.transform.position = ballRespawnPoint.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WinZone"))
        {
            backgroundMusic.volume = 0.3f;
            rigAudio.PlayOneShot(winSound);
            infoPanel.SetActive(false);
            winPanel.SetActive(true);
            
            foreach(var effect in winEffects)
            {
                effect.SetActive(true);
            }
        }
    }

}
