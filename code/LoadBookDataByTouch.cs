using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBookDataByTouch : MonoBehaviour
{
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag(tag))
        {

        }
        /*
        path = @"C:\Users\Pichau\Documents\pedeefeparapenege\books\morkborgjpgs";
        createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();*/
    }
}
