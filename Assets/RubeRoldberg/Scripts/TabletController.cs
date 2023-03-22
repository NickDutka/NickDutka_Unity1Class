using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{

    public Transform tabletController;


    void Update()
    {
        transform.rotation = tabletController.rotation;
    }
}
