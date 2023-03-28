
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MyGrabInteraction : MonoBehaviour
{
    public bool primaryButtonWasPressed;
    public bool yButtonWasPressed;
    public bool physicsEnabled = true;
    public bool transparencyEnabled = false;

    public Material transparentMaterial;
    public Material fencetopMaterial;
    public Material fencebaseMaterial;
    public Material floorbaseMaterial;


    public GameObject physicsController;
    //PhysicsSimulationController physicsSimulationController;
    VRInputActions vrInputActions;
    public GameObject mazeGameObject;

    // Transform heldObject;
    Rigidbody heldObject;

    bool didDrop;

    Vector3 previousPosition;
    Vector3 velocity;

    private void Awake()
    {
        physicsEnabled = true;
        transparencyEnabled = false;
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        if (vrInputActions.Default.LeftHandPrimary.WasPressedThisFrame())
        {
            if (physicsEnabled)
            {
                Debug.Log("X was pressed & phys enabled");
                physicsController.GetComponent<PhysicsSimulationController>().enabled = false;
                physicsEnabled = false;
            }
            else
            {
                Debug.Log("X was pressed & phys disabled");
                physicsController.GetComponent<PhysicsSimulationController>().enabled = true;
                physicsEnabled = true;
            }
        }
        // Reset Scene
        if (vrInputActions.Default.LeftHandSecondary.WasPressedThisFrame())
        {
            Debug.Log("Y Button was Pressed");

            GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");

            foreach(GameObject interactable in interactableObjects)
            {
                Destroy(interactable);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(vrInputActions.Default.RightHandSecondary.WasPressedThisFrame())
        {
            if (transparencyEnabled)
            {
                Debug.Log("B pressed");
                MeshRenderer meshRenderer = mazeGameObject.GetComponent<MeshRenderer>(); // get the MeshRenderer component
                Material[] materials = meshRenderer.materials; // get the array of materials
                for (int i = 0; i < materials.Length; i++) // loop through each material
                {
                    materials[0] = fencetopMaterial;
                    materials[1] = fencebaseMaterial;
                    materials[2] = floorbaseMaterial;
                }
                meshRenderer.materials = materials; // set the updated array of materials to the MeshRenderer component
                transparencyEnabled = false;
            }
            else
            {
                Debug.Log("B pressed");
                //Material transparentMaterial = Resources.Load<Material>("TransparentMaterial"); // load the transparent material
                MeshRenderer meshRenderer = mazeGameObject.GetComponent<MeshRenderer>(); // get the MeshRenderer component
                Material[] materials = meshRenderer.materials; // get the array of materials
                for (int i = 0; i < materials.Length; i++) // loop through each material
                {
                    materials[i] = transparentMaterial; // set the material to the transparent material
                }
                meshRenderer.materials = materials; // set the updated array of materials to the MeshRenderer component
                transparencyEnabled = true;
            }
            
        }
        

        // Grab Interaction
        if (XRController.rightHand != null)
        {
            primaryButtonWasPressed = vrInputActions.Default.RightHandPrimary.WasPressedThisFrame();
            
        }

        didDrop = false;

        if (heldObject != null)
        {
            if (primaryButtonWasPressed)
            {
                heldObject.transform.parent = null;
                heldObject.isKinematic = false;

                heldObject.velocity = velocity;

                heldObject = null;

                didDrop = true;

                primaryButtonWasPressed = false;
            }
        }

        if(primaryButtonWasPressed == true)
        {
            Debug.Log("A button was pressed");
        }
    }

    // This runs for EVERY physics step.
    private void FixedUpdate()
    {
       

        if (heldObject != null)
        {
            // Calculate the velocity in units per *frame*
            Vector3 displacement = heldObject.transform.position - previousPosition;

            // Get velocity in units per SECOND.
            velocity = displacement / Time.deltaTime;

            previousPosition = heldObject.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // OnTriggerStay runs AFTER Update, which means the button might still be "pressed",
        // resulting in us re-grabbing an object the moment we drop it.
        if (didDrop == true)
            return;

        if (heldObject == null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if (primaryButtonWasPressed)
                {

                    Debug.Log("Object was grabbed.");
                    // [x] Find any collider that is near this game object,
                    // and "grab" it.
                    other.transform.parent = transform;
                    rb.isKinematic = true;

                    heldObject = rb;

                    primaryButtonWasPressed = false;
                }
            }
        }
    }
}
