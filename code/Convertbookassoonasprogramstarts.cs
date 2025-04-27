using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Drawing;
public class Convertbookassoonasprogramstarts : MonoBehaviour
{
    // Start is called before the first frame update
    public string pdfPathIn;
    public string outputDirOut;
    public void converterLivroParaImagensPorJava(string pdfPath, string outputDir, GameObject debugReader)
    {
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                //vamo ver se funciona
                AndroidJavaClass javaClass = new AndroidJavaClass("MyClass");
                javaClass.CallStatic("setContext", currentActivity);
                debugReader.GetComponent<TMPro.TextMeshPro>().text += pdfPath + "||" + outputDir;
                string result = javaClass.CallStatic<string>("callConvertMethod", pdfPath, outputDir);
                debugReader.GetComponent<TMPro.TextMeshPro>().text += "Method to convert pdf finished..." + result;
            }
            /* try
             {

                 using (AndroidJavaClass javaClass = new AndroidJavaClass("MyClass"))
                 {
                     debugReader.GetComponent<TMPro.TextMeshPro>().text += pdfPath + "||" + outputDir;
                     string result = javaClass.CallStatic<string>("callConvertMethod", pdfPath, outputDir);
                     debugReader.GetComponent<TMPro.TextMeshPro>().text += "provavelmente funcionou, segue resultado: " + result;
                 }
             }*/
        }
        catch (Exception e)
        {
            //legitima impressão que esse catch seja inútil já que o erro é de chamada de java, portanto não retorna outra coisa senão 'deu erro no java'
            Debug.Log("Problem when calling pdfConverter: " + e.Message);
            debugReader.GetComponent<TMPro.TextMeshPro>().text += "Problem when calling pdfConverter: " + e.Message;
        }
    }

    /*void convertaolivroemimgs(string pdfFilePath, string outputDirectory)
    {
        dynamic sys = Py.Import("sys");
        sys.path.append(Application.dataPath);
        Runtime.PythonDLL = Application.streamingAssetsPath + "/pythonebend/python312.dll";
        Debug.Log("PythonDLL: " + Runtime.PythonDLL);
        PythonEngine.Initialize();
        using (Py.GIL())
        {
            var pythonScript = Py.Import("convertaolivroemimgs");
            var message = new PyString(pdfFilePath);
            var result = pythonScript.InvokeMethod("mainene", new PyObject[] { message });
            Debug.Log(result);
        }
        /*if (!File.Exists(pdfFilePath))
        {
            Debug.LogError("PDF file not found: " + pdfFilePath);
            return;
        }

        using (var document = PdfDocument.Load(pdfFilePath))
        {
            for (int page = 0; page < document.PageCount; page++)
            {
                using (var image = document.Render(page, 300, 300, PdfRenderFlags.CorrectFromDpi))
                {
                    string outputFileName = Path.Combine(outputDirectory, $"page-{page + 1}.png");
                    image.Save(outputFileName, System.Drawing.Imaging.ImageFormat.Png);
                    Debug.Log($"Saved page {page + 1} to {outputFileName}");
                }
            }
        }
        */

}