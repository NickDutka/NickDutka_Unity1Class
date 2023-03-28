using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeBehaviour : MonoBehaviour
{
    public float rupeeRotationSpeed = 1f;
    void Update()
    {
        transform.Rotate(Vector3.up, rupeeRotationSpeed * Time.deltaTime);
    }
}
