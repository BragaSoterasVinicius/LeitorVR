using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractor : XRDirectInteractor
{
    private bool ManterObjetoSegurado = true;

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (ManterObjetoSegurado)
        {
            return;
        }
        base.OnSelectExited(args);
    }
    public void ReleaseObject()
    {
        ManterObjetoSegurado = false;
    }
}
