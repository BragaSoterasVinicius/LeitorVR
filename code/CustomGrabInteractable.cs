using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class CustomGrabInteractable : XRGrabInteractable
{
    public InputActionProperty pressGripAction;
    public InputActionProperty pressButtonAAction;

    public GameObject debugReader;
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        debugReader.GetComponent<TextMeshPro>().text = "largayy";
        /*
        if (pressButtonAAction.action.triggered)
        {
            debugReader.GetComponent<TextMeshPro>().text = "apertou A";
            //interactionManager.SelectExit(directInteractor, heldObject);
            interactionManager.CancelInteractorSelection(args.interactor);
        }
        if (pressGripAction.action.triggered)
        {
            debugReader.GetComponent<TextMeshPro>().text = "apertou grip";
            interactionManager.SelectEnter(args.interactor, this);
            return;
        }*/
    }
}
