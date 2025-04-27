using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugraycast : MonoBehaviour
{
    void Update()
    {
        Vector3 rayOrigin = transform.position; // E.g., controller or camera position
        Vector3 rayDirection = transform.forward; // E.g., forward direction of the controller

        // Draw the ray in the Scene view
        Debug.DrawRay(rayOrigin, rayDirection * 10, Color.red);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, 10))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.name);

            // Traverse the hierarchy to find the Text component
            Text uiText = hitInfo.collider.GetComponentInChildren<Text>();
            if (uiText != null)
            {
                Debug.Log("Text found: " + uiText.text);
            }
            else
            {
                Debug.Log("No Text component found in the hit object or its children.");
            }
        }
    }

}
