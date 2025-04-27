using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NaoLargaraOLivroQuandoPressionado : MonoBehaviour
{
    public InputActionProperty gripAction;
    public colinhaDeCriaQuandoPegoLivro objectHolder; // Reference to the other script


    void Update()
    {
        if (gripAction.action.triggered && objectHolder.IsHoldingObject())
        {
            GameObject heldObject = objectHolder.GetHeldObject();
            if (heldObject != null)
            {
                objectHolder.grabObject(heldObject.GetComponent<XRGrabInteractable>());
            }
        }
    }
}
