using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
using System.IO;
public class abrirtelaparaconverterpdfparajpgsevirarlivro : MonoBehaviour
{
    public GameObject debugReader;
    string path;

    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            //debugReader.GetComponent<TextMeshPro>().text = "openmenuPDFtoJPG";
            FileBrowser.SetFilters(true, new FileBrowser.Filter("PDF", ".pdf"));
            FileBrowser.SetDefaultFilter(".pdf");
            FileBrowser.ShowLoadDialog((paths) =>
            {
                path = paths[0];
                string directoryPath = Path.GetDirectoryName(path);
                //debugReader.GetComponent<TextMeshPro>().text += "Selected: " + paths[0];
                //converter pdf para jpg
                Convertbookassoonasprogramstarts convertbookassoonasprogramstartsInstance =
                GetComponent<Convertbookassoonasprogramstarts>();
                //definir uma pasta para salvar as imagens, ao invés dessa estranheza de ser na pasta do livro/books/nome_do_livro
                //path deve ser o caminho do livro, sem o nome do livro.
                convertbookassoonasprogramstartsInstance
                .converterLivroParaImagensPorJava(path, directoryPath, debugReader);
                //debugReader.GetComponent<TextMeshPro>().text += "Convertendo jpg para livro";
                //criar livro
                //criar páginas
                //adicionar páginas ao livro
            },
            () => { debugReader.GetComponent<TextMeshPro>().text = "Canceled"; },
            FileBrowser.PickMode.Files, false, null, null, "Select PDF", "Select");
        }
    }
}
