using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;
public class RenderPages : MonoBehaviour
{
    public void LoadTexturesForThePage(int pagenumberstart, int pagenumberend, string libpath, GameObject book, GameObject debugReader)
    {
        debugReader.GetComponent<TextMeshPro>().text = "Loading textures for pages: " + pagenumberstart + " to " + pagenumberend;
        for (int i = pagenumberstart; i <= pagenumberend; i++)
        {
            Material material = new Material(Shader.Find("Standard"));
            string imagePath = (libpath + "/page_" + i + ".png");
            //A partir daqui ele tem que encontrar o objeto da pagina dentro do objeto do livro (criar um pageObj)
            try
            {
                GameObject pageObj = new GameObject();
                //tem um jeito melhor de escrever isso pra diminuir a complexidade, não vou fazer por enquanto

                if ((i % 2) == 0)
                {
                    string gameObjectName = "spine_page_" + i;
                    GameObject pageSpine = book.transform.Find(gameObjectName).gameObject;
                    pageObj = pageSpine.transform.GetChild(0).gameObject;
                }
                else
                {
                    string gameObjectName = "spine_page_" + (i - 1);
                    GameObject pageSpine = book.transform.Find(gameObjectName).gameObject;
                    GameObject pageObjPar = pageSpine.transform.GetChild(0).gameObject;
                    pageObj = pageObjPar.transform.GetChild(0).gameObject;
                }
                StartCoroutine(LoadImage(imagePath, material, pageObj, debugReader));
            }
            catch (Exception e) { Debug.Log("Erro ao criar pageObj: " + e); }
        }
    }

    private IEnumerator LoadImage(string filePath, Material material, GameObject obj, GameObject debugReader)
    {
        filePath = Uri.EscapeUriString(filePath);
        debugReader.GetComponent<TextMeshPro>().text += "Loading image: " + filePath;
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                debugReader.GetComponent<TextMeshPro>().text += "Failed to load texture: " + uwr.error;
                Debug.LogError("Failed to load texture: " + uwr.error);
                debugReader.GetComponent<TextMeshPro>().text += "Failed to result:  " + uwr.result;
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                material.mainTexture = texture;
                Debug.Log("Loaded texture: " + texture.width + "x" + texture.height);
                debugReader.GetComponent<TextMeshPro>().text += "Loaded texture: " + texture.width + "x" + texture.height;
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                renderer.material = material;
            }
        }
    }

    public void UnloadTexturesForThePage(int pagenumberstart, int pagenumberend, GameObject book, GameObject debugReader)
    {
        debugReader.GetComponent<TextMeshPro>().text = "Unloading textures for pages: " + pagenumberstart + " to " + pagenumberend;
        for (int i = pagenumberstart; i < pagenumberend; i++)
        {
            if (i == 0) { continue; }
            try
            {
                GameObject pageObj = new GameObject();
                if ((i % 2) == 0)
                {
                    string gameObjectName = "spine_page_" + i;
                    GameObject pageSpine = book.transform.Find(gameObjectName).gameObject;
                    pageObj = pageSpine.transform.GetChild(0).gameObject;
                }
                else
                {
                    string gameObjectName = "spine_page_" + (i - 1);
                    GameObject pageSpine = book.transform.Find(gameObjectName).gameObject;
                    GameObject pageObjPar = pageSpine.transform.GetChild(0).gameObject;
                    pageObj = pageObjPar.transform.GetChild(0).gameObject;
                }
                MeshRenderer renderer = pageObj.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.mainTexture = null;
                }
            }
            catch (Exception e) { Debug.Log("Erro ao criar pageObj: " + e); }
        }
        Resources.UnloadUnusedAssets();
    }

}
