using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveBookController : MonoBehaviour
{
    public GameObject otherButton;
    public GameObject debugReader;
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag(tag))
        {
            debugReader.GetComponent<TMPro.TextMeshPro>().text = "Saving book...";
            ButtonsServiceSaveLoad buttonsServiceSaveLoad = GetComponent<ButtonsServiceSaveLoad>();
            if (buttonsServiceSaveLoad.otherButton != null)
            {
                debugReader.GetComponent<TMPro.TextMeshPro>().text += "otherButton found!";
            }
            buttonsServiceSaveLoad.SaveBook(debugReader, otherButton);
        }
    }
}
