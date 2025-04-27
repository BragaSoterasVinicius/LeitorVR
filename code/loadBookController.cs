using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadBookController : MonoBehaviour
{
    public GameObject debugReader;
    public GameObject book;
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag(tag))
        {
            ButtonsServiceSaveLoad buttonsServiceSaveLoad = GetComponent<ButtonsServiceSaveLoad>();
            buttonsServiceSaveLoad.LoadBooks(book, debugReader);
        }

    }
}
