using UnityEngine;
using System.Collections;

public class ObjectLifecycle : MonoBehaviour
{
    public GameObject objectToCopy;

    private void Start()
    {
        // StartCoroutine(MakeEmptyGO());

        GameObject objectWeCreated =
        //                                                  this just means "default rotation"
            Instantiate(objectToCopy, new Vector3(5, 5, 5), Quaternion.identity);
    }

    //private T InstantiateABunch<T>(T clone) where T : Object
    //{

    //}

    private IEnumerator MakeEmptyGO()
    {
        // Create a new empty game object.
        GameObject myNewGameObject = new GameObject("My Game Object!");

        BoxCollider myBoxCollider = myNewGameObject.AddComponent<BoxCollider>();
        myNewGameObject.AddComponent<Rigidbody>();

        // myNewGameObject.GetComponent<Transform>()...
        myNewGameObject.transform.position = new Vector3(0, 10, 0);
        myNewGameObject.transform.eulerAngles = new Vector3(45, 45, 45);

        yield return new WaitForSeconds(3);

        // You can destroy components, too. Be careful what you pass in to Destroy()!
        // Destroy(myBoxCollider);
        Destroy(myNewGameObject);
    }
}
