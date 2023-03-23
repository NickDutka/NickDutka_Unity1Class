using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{

    public Transform tabletController;

    public float rotateSpeedFactor = 1;


    private void LateUpdate()
    {
        Quaternion tabletControllerRotation = tabletController.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, tabletControllerRotation, rotateSpeedFactor * Time.deltaTime);

    }
}
