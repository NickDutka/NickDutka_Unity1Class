using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;

public class ObjectSelectionAR : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject selectedObject;
    public Camera Camera;
    public Button deleteButton;
    public Button moveRightButton;
    public Button moveLeftButton;
    public Button moveUpButton;
    public Button moveDownButton;
    public Button rotateRightButton;
    public Button rotateLeftButton;

    public float moveAmount = 1f;
    public float rotationAmount = 45f;



    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        deleteButton.onClick.AddListener(OnDeleteButtonClick);
        moveRightButton.onClick.AddListener(OnMoveRightButtonClick);
        moveLeftButton.onClick.AddListener(OnMoveLeftButtonClick);
        moveUpButton.onClick.AddListener(OnMoveUpButtonClick);
        moveDownButton.onClick.AddListener(OnMoveDownButtonClick);
        rotateRightButton.onClick.AddListener(OnRotateRightButtonClick);
        rotateLeftButton.onClick.AddListener(OnRotateLeftButtonClick);
    }


    void Update()

    {
        Vector2 pos;

#if UNITY_EDITOR
        if (Mouse.current.leftButton.wasPressedThisFrame == false)
            return;

        pos = Mouse.current.position.ReadValue();
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                return;
            }
        }

        pos = Input.GetTouch(0).position;

#endif
        Debug.Log($"Clicked: {pos}");

        Ray ray = Camera.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {

            GameObject tappedObject = hitInfo.transform.gameObject;

            // Check if the tapped object is selectable (you can modify this check as per your requirements)
            if (tappedObject.GetComponent<SelectableObject>() != null)
            {
                // Select the object
                SelectObject(tappedObject);
                Debug.Log("selected object");
            }

        }
        
    }

    void SelectObject(GameObject objectToSelect)
    {
        if (selectedObject != null)
        {
            // Deselect the currently selected object if there is one
            DeselectObject();
        }


        // Store the selected object
        selectedObject = objectToSelect;
        

        // Perform any additional actions or effects on the selected object if needed

        // Example: Change the material color of the selected object
        Renderer objectRenderer = selectedObject.GetComponent<Renderer>();
        objectRenderer.material.color = Color.green;
    }

    void DeselectObject()
    {
        // Perform any actions or effects to revert the changes on the previously selected object if needed

        // Example: Reset the material color of the deselected object
        Renderer objectRenderer = selectedObject.GetComponent<Renderer>();
        objectRenderer.material.color = Color.white;

        // Clear the selected object
        selectedObject = null;
        Debug.Log("deselected object");
    }
    void OnDeleteButtonClick()
    {
        if (selectedObject != null)
        {
            // Delete the selected object
            DeleteObject(selectedObject);
            selectedObject = null;
        }
    }

    void OnMoveRightButtonClick()
    {
        if (selectedObject != null)
        {
            // Move the selected object to the right
            MoveObject(selectedObject, Vector3.right * moveAmount);
        }
    }
    void OnMoveLeftButtonClick()
    {
        if (selectedObject != null)
        {
            // Move the selected object to the left
            MoveObject(selectedObject, Vector3.left * moveAmount);
        }
    }
    void OnMoveUpButtonClick()
    {
        if (selectedObject != null)
        {
            // Move the selected object to the left
            MoveObject(selectedObject, Vector3.forward * moveAmount);
        }
    }

    void OnMoveDownButtonClick()
    {
        if (selectedObject != null)
        {
            // Move the selected object to the left
            MoveObject(selectedObject, Vector3.back * moveAmount);
        }
    }

    void OnRotateRightButtonClick()
    {
        if (selectedObject != null)
        {
            // Rotate the selected object to the right
            RotateObject(selectedObject, Vector3.up, rotationAmount);
        }
    }
    void OnRotateLeftButtonClick()
    {
        if (selectedObject != null)
        {
            // Rotate the selected object to the right
            RotateObject(selectedObject, Vector3.down, rotationAmount);
        }
    }
    void DeleteObject(GameObject objectToDelete)
    {
        // Perform any necessary cleanup or additional logic before deleting the object

        Destroy(objectToDelete);
    }

    void MoveObject(GameObject objectToMove, Vector3 direction)
    {
        // Move the object in the specified direction
        objectToMove.transform.Translate(direction);
    }
    void RotateObject(GameObject objectToRotate, Vector3 axis, float angle)
    {
        // Rotate the object around the specified axis by the specified angle
        objectToRotate.transform.Rotate(axis, angle);
    }

}
