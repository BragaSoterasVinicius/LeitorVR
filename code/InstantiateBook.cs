using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class InstantiateBook : MonoBehaviour
{
    private XRInteractionManager interactionManager;
    public GameObject debugReader;
    void Start()
    {
        interactionManager = FindObjectOfType<XRInteractionManager>();
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null && interactionManager != null)
        {
            grabInteractable.interactionManager = interactionManager;
        }
        else
        {
            debugReader.GetComponent<TextMeshPro>().text = "No XRInteractionManager or XRGrabInteractable found.";
            Debug.LogError("No XRInteractionManager or XRGrabInteractable found.");
        }
    }
}
