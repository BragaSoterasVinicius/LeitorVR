using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
public class FileBrowserTest : MonoBehaviour
{
    public GameObject debugReader;
    // Start is called before the first frame update
    public GameObject book;

    public void openMenuForLoadingImages()
    {

        string path = "";
        //debugReader.GetComponent<TextMeshPro>().text += "Testando abertura de menu";
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.ShowLoadDialog((paths) =>
        {
            path = paths[0];
            createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();
            createPagesByPathInstance.LoadBook(path, debugReader, book);
            Debug.Log("Selected: " + paths[0]);

        },
        () => { debugReader.GetComponent<TextMeshPro>().text += "Canceled"; },
        FileBrowser.PickMode.Folders, false, null, null, "Select Book Folder", "Select");
    }


    void Start()
    {
        openMenuForLoadingImages();
        /*
        path = @"C:\Users\Pichau\Documents\pedeefeparapenege\books\morkborgjpgs";
        createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();*/


    }
}
