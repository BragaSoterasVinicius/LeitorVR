using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabandopen : MonoBehaviour
{
    private XRBaseInteractor interactor;
    private bool isSegurado = false;
    private Animator animate;
    public GameObject targetObject;

    void Start()
    {
        animate = targetObject.GetComponent<Animator>();
    }
    public void OnSelectEnter(XRBaseInteractor interactor)
    {
        this.interactor = interactor;
        isSegurado = true;
    }
    public void onSelectExit(XRBaseInteractor interactor)
    {
        if (this.interactor == interactor)
        {
            this.interactor = null;
            isSegurado = false;
        }
    }
    void Update()
    {
        /* if (isSegurado && interactor != null)
         {
             if (interactor.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool isPressed) && isPressed)
             {
                 // Play animation
                 animate.SetTrigger("PlayAnimation");
             }
         }*/
    }
}
