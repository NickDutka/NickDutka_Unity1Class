using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuessingGame : MonoBehaviour
{
    public TextMeshProUGUI resultTextGameObject;
    public TMP_InputField inputFieldGameObject;
    public TMP_InputField guessFieldGameObject;
    private int guessnumber;
    private int upperbounds;
    private int magicnumber;
   

    public void FindRandomNumber()
    {
        
        upperbounds = int.Parse(inputFieldGameObject.text);
        Debug.Log(upperbounds);

        magicnumber = Random.Range(0, upperbounds);
        Debug.Log(magicnumber);
    }

    public void CheckGuess()
    {
        guessnumber = int.Parse(guessFieldGameObject.text);

        if (guessnumber == magicnumber)
        {
            resultTextGameObject.text = "Correct!";
        }
        else if (guessnumber < magicnumber)
        {
            resultTextGameObject.text = "incorrect, too low! Try again.";
        }
        else if (guessnumber > magicnumber)
        {
            resultTextGameObject.text = "incorrect, too high! Try again.";
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
