using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //static bool isPersistent = false;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerRig");

        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        //if (!isPersistent)
        //{
        //    // This will prevent the GameObject this script is attached to from being destroyed when a new scene loads.
        //    DontDestroyOnLoad(gameObject);

        //    isPersistent = true;
        //}
        //else if(!transform.IsChildOf(GameObject.Find("RigControledViaCode").transform))
        //{
        //    // Destroy any duplicates of the persistent object that may be created when the scene is reloaded.
        //    Destroy(gameObject);
        //}
        
    }
}
    
