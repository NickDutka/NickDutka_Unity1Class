using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRunner : MonoBehaviour
{
    public MonoBehaviour runnerScript;

    public void enableRunners()
    {
        runnerScript.enabled = true;
    }
}
