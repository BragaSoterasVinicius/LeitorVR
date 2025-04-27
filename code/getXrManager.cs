using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class getXrManager : MonoBehaviour
{
    private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        // Find the Interaction Manager in the scene (assuming only one exists)
        XRInteractionManager interactionManager = FindObjectOfType<XRInteractionManager>();

        if (interactionManager != null)
        {
            interactable.interactionManager = interactionManager;
        }
        else
        {
            Debug.LogError("No XR Interaction Manager found in the scene.");
        }
    }
}