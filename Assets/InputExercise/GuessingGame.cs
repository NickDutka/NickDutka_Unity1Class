using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuessingGame : MonoBehaviour
{
    public TextMeshProUGUI textGameObject;
    public TMP_InputField inputFieldGameObject;
    public void MyFunction()
    {
        textGameObject.text = "Hello " + inputFieldGameObject.text + " you are " + Random.Range(1, 100) + " years old.";
        
        
        Debug.Log("Goodbye");
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
