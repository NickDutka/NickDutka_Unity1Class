
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class MyVrButtonInput : MonoBehaviour
{
    VRInputActions vrInputActions;

    private void Awake()
    {
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        // OculusTouchController right = (OculusTouchController)XRController.rightHand;
        // ViveWand rightViveWand = (ViveWand)XRController.rightHand;

        //if (right != null)
        //{
        //    if (right.primaryButton.wasPressedThisFrame /*|| rightViveWand.touchpad.wasPressedThisFrame*/)
        //    {
        //        Debug.Log("Click!");
        //    }
        //}

        if (vrInputActions.Default.Primary.WasPressedThisFrame())
        {
            Debug.Log("Click");
        }
    }
}
