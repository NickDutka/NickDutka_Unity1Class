
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class MyGrabInteraction : MonoBehaviour
{
    public bool primaryButtonWasPressed;

    VRInputActions vrInputActions;

    // Transform heldObject;
    Rigidbody heldObject;

    bool didDrop;

    Vector3 previousPosition;
    Vector3 velocity;

    private void Awake()
    {
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        if (XRController.rightHand != null)
        {
            primaryButtonWasPressed = vrInputActions.Default.Primary.WasPressedThisFrame();
            
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
