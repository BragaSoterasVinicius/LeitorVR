using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class colinhaDeCriaQuandoPegoLivro : MonoBehaviour
{
    public InputActionProperty gripAction;
    public XRRayInteractor rayInteractor;
    public XRDirectInteractor directInteractor;
    private XRGrabInteractable heldObject = null;
    private bool isHolding = false;
    public GameObject debugReader;


    void Update()
    {
        if (gripAction.action.triggered)
        {
            if (isHolding)
            {
                ReleaseObject();
            }
            else
            {
                TryPickupObject();
            }
        }
    }

    void TryPickupObject()
    {
        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            debugReader.GetComponent<TextMeshPro>().text += "touched";
            XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
            if (interactable != null)
            {
                debugReader.GetComponent<TextMeshPro>().text += "the object is interactable";
                grabObject(interactable);
            }
        }
    }

    public void grabObject(XRGrabInteractable interactable)
    {
        if (interactable != null)
        {
            debugReader.GetComponent<TextMeshPro>().text += "the object is interactable";
            directInteractor.interactionManager.SelectEnter(directInteractor, interactable);

            heldObject = interactable;

            isHolding = true;

            // Attach to interactor
            //mudar para que o directInteractor rode ao invés do livro
            heldObject.transform.SetParent(directInteractor.transform);

            heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while held

        }
    }
    void ReleaseObject()
    {
        if (heldObject != null)
        {
            directInteractor.interactionManager.SelectExit(directInteractor, heldObject);
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics

            heldObject = null;
            isHolding = false;
        }
    }

    public bool IsHoldingObject()
    {
        return isHolding;
    }
    public GameObject GetHeldObject()
    {
        return heldObject != null ? heldObject.gameObject : null;
    }
}