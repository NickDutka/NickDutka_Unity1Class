using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClickCreateObject : MonoBehaviour
{
    public Camera cam;
    public GameObject objPrefab;
    public List <GameObject> objects;
    public TMP_Dropdown dropdown;
    public int index;
    public bool hasClicked;

    private Quaternion planarRotation2 = Quaternion.Euler(0f, -90f, 0f);

    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
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
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                return;
            }
        }

        pos = Input.GetTouch(0).position;
#endif

        Debug.Log($"Clicked: {pos}");

        Ray ray = cam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            bool canPlaceObject = CanPlaceObject(hitInfo.collider.gameObject, hitInfo.transform.rotation);

            if (canPlaceObject && hitInfo.collider.gameObject.GetComponent<SelectableObject>() == null && index >= 0 && index < objects.Count)
            {
                GameObject prefab = objects[index];
                GameObject instantiatedObject = Instantiate(prefab, hitInfo.point + new Vector3(0, 0.2f, 0), Quaternion.identity);

                Debug.Log("Created Object");
                
                // Perform any additional setup or modifications to the instantiated object if needed
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
}
