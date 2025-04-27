using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleGrabInteractable : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Start()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Add event listeners for grab and release
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // When grabbed, set the object to "stick" to the hand
        isGrabbed = true;
        Debug.Log("Object Grabbed: Sticking to hand.");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Only release if not toggled on
        if (!isGrabbed)
        {
            Debug.Log("Object Released.");
        }
        else
        {
            // Re-attach the object to the hand
            grabInteractable.interactionManager.SelectEnter(args.interactorObject, grabInteractable);
            Debug.Log("Object Re-attached: Still sticking to hand.");
        }
    }

    public void ToggleGrab()
    {
        // Toggle the grab state
        isGrabbed = !isGrabbed;

        if (!isGrabbed)
        {
            // Force release the object
            grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
            Debug.Log("Object Toggled Off: Released from hand.");
        }
    }
}