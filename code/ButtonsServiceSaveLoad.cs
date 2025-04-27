using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonsServiceSaveLoad : MonoBehaviour
{

    public GameObject otherButton;
    //Load -> Pegar o index, e por index instanciar cada um dos livros salvos
    public void LoadBooks(GameObject book, GameObject debugReader)
    {
        int index = SaveAndLoadBookSys.loadBookIndex();
        debugReader.GetComponent<TextMeshPro>().text = "Books to load: " + index;
        for (int i = 0; i < index; i++)
        {
            BookData data = SaveAndLoadBookSys.loadBook(i);
            createPagesByPath createPagesByPathInstance = GetComponent<createPagesByPath>();
            createPagesByPathInstance.LoadBook(data.bookPath, debugReader, book);
        }
        debugReader.GetComponent<TextMeshPro>().text += "Books loaded!";
    }
    public void SaveBook(GameObject debugReader, GameObject otherButton)
    {
        debugReader.GetComponent<TextMeshPro>().text = "Saving book 2...";
        int index = SaveAndLoadBookSys.loadBookIndex();
        debugReader.GetComponent<TextMeshPro>().text = "index: " + index;
        string returno = SaveAndLoadBookSys.saveBookData(index + 1, otherButton.GetComponent<createPagesByPath>().libpathexternal);
        string returno2 = SaveAndLoadBookSys.saveBookIndex(index + 1);
        debugReader.GetComponent<TextMeshPro>().text = "Book and index saved! index: " + (index + 1) + " returno: " + returno + " returno2: " + returno2;
    }

    //Save -> Pegar o index, salvar o livro o ultimo, e incrementar o index
}
