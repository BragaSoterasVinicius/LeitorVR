using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
public class FileBrowserByCollision : MonoBehaviour
{
    public GameObject debugReader;
    // Start is called before the first frame update
    public GameObject book;

    public string tag;
    public void openMenuForLoadingImages()
    {

        string path = "";
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.ShowLoadDialog((paths) =>
        {
            path = paths[0];
            debugReader.GetComponent<TextMeshPro>().text += paths[0];
            createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();
            createPagesByPathInstance.LoadBook(path, debugReader, book);
            Debug.Log("Selected: " + paths[0]);

        },
        () => { debugReader.GetComponent<TextMeshPro>().text += "Canceled"; },
        FileBrowser.PickMode.Folders, false, null, null, "Select Book Folder", "Select");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag(tag))
        {
            openMenuForLoadingImages();
        }
        /*
        path = @"C:\Users\Pichau\Documents\pedeefeparapenege\books\morkborgjpgs";
        createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();*/


    }
}
