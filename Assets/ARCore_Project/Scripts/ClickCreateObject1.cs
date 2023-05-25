using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClickCreateObject1 : MonoBehaviour
{
    public Camera cam;
    public GameObject objPrefab;
    public List <GameObject> objects;
    public TMP_Dropdown dropdown;
    public int index;
    public bool hasClicked;

    private GameObject selectedObject;

    public Button deleteButton;
    public Button moveRightButton;
    public Button moveLeftButton;
    public Button moveUpButton;
    public Button moveDownButton;
    public Button rotateRightButton;
    public Button rotateLeftButton;

    public float moveAmount = 1f;
    public float rotationAmount = 45f;

    private Quaternion planarRotation2 = Quaternion.Euler(0f, -90f, 0f);

    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        deleteButton.onClick.AddListener(OnDeleteButtonClick);
        moveRightButton.onClick.AddListener(OnMoveRightButtonClick);
        moveLeftButton.onClick.AddListener(OnMoveLeftButtonClick);
        moveUpButton.onClick.AddListener(OnMoveUpButtonClick);
        moveDownButton.onClick.AddListener(OnMoveDownButtonClick);
        rotateRightButton.onClick.AddListener(OnRotateRightButtonClick);
        rotateLeftButton.onClick.AddListener(OnRotateLeftButtonClick);
    }
    private void Update()
    {

        Vector2 pos;

#if UNITY_EDITOR
        if (Mouse.current.leftButton.wasPressedThisFrame == false)
            return;

        pos = Mouse.current.position.ReadValue();
#else
if (Input.touchCount > 0)
{
    UnityEngine.Touch touch = Input.GetTouch(0);

    if (touch.phase == UnityEngine.TouchPhase.Began)
    {
        return;
    }
    
    pos = touch.position;
}
else
{
    return;
}
#endif

        Debug.Log($"Clicked: {pos}");

        Ray ray = cam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {

            // Check if the raycast hit intersects with any UI elements
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0))
            {
                Debug.Log("Clicked on UI element, ignoring object placement.");
                return;
            }

            bool canPlaceObject = CanPlaceObject(hitInfo.collider.gameObject, hitInfo.transform.rotation);

            if (canPlaceObject && hitInfo.collider.gameObject.GetComponent<SelectableObject>() == null && index >= 0 && index < objects.Count)
            {
                GameObject prefab = objects[index];
                GameObject instantiatedObject = Instantiate(prefab, hitInfo.point + new Vector3(0, 0.2f, 0), Quaternion.identity);

                Debug.Log("Created Object");
                
                // Perform any additional setup or modifications to the instantiated object if needed
            }

            GameObject tappedObject = hitInfo.transform.gameObject;

            // Check if the tapped object is selectable (you can modify this check as per your requirements)
            if (tappedObject.GetComponent<SelectableObject>() != null)
            {
                // Select the object
                SelectObject(tappedObject);
                Debug.Log("selected object");
            }

            //Instantiate(objPrefab, hitInfo.point + new Vector3(0, 0.2f, 0), Random.rotation);

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1);

        }
    }

    private bool CanPlaceObject(GameObject surfaceObject, Quaternion surfaceRotation)
    {
        if (IsPlanePlanar(surfaceRotation))
        {
            // Check if the selected object can be placed on planar surfaces
            if (index >= 0 && index < objects.Count)
            {
                GameObject prefab = objects[index];
                bool canPlaceOnPlanarSurface = prefab.GetComponent<PlaceOnPlanarSurface>() != null;
                return canPlaceOnPlanarSurface;
            }
        }
        else
        {
            // Check if the selected object can be placed on vertical surfaces
            if (index >= 0 && index < objects.Count)
            {
                GameObject prefab = objects[index];
                bool canPlaceOnVerticalSurface = prefab.GetComponent<PlaceOnVerticalSurface>() != null;
                return canPlaceOnVerticalSurface;
            }
        }

        return false;
    }

    private bool IsPlanePlanar(Quaternion rotation)
    {
        // Compare with planar rotation
        if (rotation.eulerAngles == planarRotation2.eulerAngles)
        {
            // Plane is planar
            return true;
        }
        else
        {
            // Plane is vertical
            return false;
        }
    }

    private void OnDropdownValueChanged(int value)
    {
        index = value;
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
