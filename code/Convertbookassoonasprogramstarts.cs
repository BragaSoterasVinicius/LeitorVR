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
        }
        catch (Exception e)
        {
            //legitima impressão que esse catch seja inútil já que o erro é de chamada de java, portanto não retorna outra coisa senão 'deu erro no java'
            Debug.Log("Problem when calling pdfConverter: " + e.Message);
            debugReader.GetComponent<TMPro.TextMeshPro>().text += "Problem when calling pdfConverter: " + e.Message;
        }
    }
}