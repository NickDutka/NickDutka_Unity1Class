using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTablet : MonoBehaviour
{

    public Transform tabletResetTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TabletReset"))
        {
            gameObject.transform.position = tabletResetTransform.position;
            gameObject.transform.rotation = tabletResetTransform.rotation;
        }
    }
}
