using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraObject;
    public Transform[] playerObjects;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = float.MaxValue;
        Transform closestObject = null;
        foreach (Transform objectToMeasureTo in playerObjects)
        {
            float distance = Mathf.Abs(objectToMeasureTo.position.z - cameraObject.position.z);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = objectToMeasureTo;
            }
        }

        if (closestObject != null)
        {
            if (closestDistance > 10)
            {
                cameraObject.position += new Vector3(0, 0, 5);
            }
            //else
            //{
            //    cameraObject.position = new Vector3(
            //        cameraObject.position.x,
            //        cameraObject.position.y,
            //        closestObject.position.z
            //    );
            //}
        }
    }
}
