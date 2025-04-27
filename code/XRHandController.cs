using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class XRHandController : MonoBehaviour
{
    public XRNode handType; // LeftHand or RightHand
    private InputDevice device;

    public XRRayInteractor rayInteractor; // Assign this in the Inspector
    public LayerMask teleportLayer;
    public TeleportationProvider teleportationProvider; // Assign this in the Inspector

    private XRGrabInteractable grabbedObject = null;
    private XRDirectInteractor interactor; // Used to simulate grabbing

    public GameObject debugReader;
    void Start()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(handType);

        // Detect Hold (Grip) for grabbing
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool isHolding) && isHolding)
        {
            if (grabbedObject == null)
                TryGrabObject();
        }
        else
        {
            ReleaseObject();
        }

        // Detect Trigger for teleport
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTeleporting) && isTeleporting)
        {
            TryTeleport();
        }
    }

    void TryGrabObject()
    {
        debugReader.GetComponent<TextMeshPro>().text += "Livro pego";
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
            if (interactable != null)
            {
                grabbedObject = interactable;
                interactor.interactionManager.SelectEnter(interactor, grabbedObject);
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            interactor.interactionManager.SelectExit(interactor, grabbedObject);
            grabbedObject = null;
        }
    }

    void TryTeleport()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) && ((1 << hit.collider.gameObject.layer) & teleportLayer) != 0)
        {
            TeleportRequest request = new TeleportRequest()
            {
                destinationPosition = hit.point
            };
            teleportationProvider.QueueTeleportRequest(request);
        }
    }
}
