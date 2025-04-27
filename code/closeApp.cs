using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeApp : MonoBehaviour
{
    public void getOUT()
    {
        StartCoroutine(WaitAndQuit());
    }

    IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(1.2f);
        Application.Quit();
    }
}
