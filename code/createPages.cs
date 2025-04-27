using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
public class createPages : MonoBehaviour
{
    public GameObject book;
    // Start is called before the first frame update
    public string libpath;

    public float distanceAmongPages;
    public void LoadBook(string bookname)
    {
        libpath = libpath.Replace("\\", "/");
        int TotalFolderFiles = Directory.GetFiles(libpath + "/books/" + bookname).Length;
        GameObject lastPage = null;
        Vector3 parentPosition = book.transform.position;
        Debug.Log("TotalFolderFiles: " + TotalFolderFiles);
        book.GetComponent<BoxCollider>().size = new Vector3(0.20923f, distanceAmongPages * (float)TotalFolderFiles * 0.01f, 0.29f);
        book.GetComponent<BoxCollider>().center = new Vector3(0f, -distanceAmongPages * (float)TotalFolderFiles * 0.01f / 2f, 0f);
        //pegue as páginas da pasta de livros, 
        //crie um objeto para cada página e adicione a imagem da página ao objeto
        //adicione o objeto ao corpo do livro

        //importante lembrar que cada folha tem duas páginas, então, por i, vai haver uma página normal e a seguinte ainda vai estar
        // na mesma coordenada y, apenas rodando 180 graus no eixo z
        for (int i = 0; i < TotalFolderFiles; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.name = "page_" + i;
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                Destroy(collider);
            }

            if ((i % 2) != 0)
            {
                obj.transform.position = new Vector3(parentPosition.x, parentPosition.y - (float)(i - 1) * distanceAmongPages / 100, parentPosition.z);
                obj.transform.Rotate(0, 0, 180);
            }
            else
            {
                obj.transform.position = new Vector3(parentPosition.x, parentPosition.y - (float)i * distanceAmongPages / 100, parentPosition.z);
            }
            Debug.Log("obj_y: " + obj.transform.position.y);
            obj.transform.localScale = new Vector3(0.02f, 0.01f, 0.03f);
            Debug.Log("obj: " + obj.transform.position);
            Material material = new Material(Shader.Find("Standard"));

            string imagePath = (libpath + "/books/" + bookname + "/page_" + i + ".jpg");
            StartCoroutine(LoadImage(imagePath, material));
            /*Texture2D texture = Resources.Load<Texture2D>(libpath + "/books/" + bookname + "/page_" + i);
            Debug.Log("texture: " + texture.width + " " + texture.height);
            material.mainTexture = texture;
            Debug.Log("material: " + material.mainTexture.width + " " + material.mainTexture.height);
            */
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = material;
            if ((i % 2) != 0)
            {
                obj.transform.SetParent(lastPage.transform);
            }
            else
            {
                obj.transform.SetParent(book.transform);
                lastPage = obj;
                createFatherSpine(obj);
            }
            /*
            obj.transform.SetParent(book.transform);*/
        }


        /*
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(0.02f, 0.01f, 0.03f);
        Debug.Log("obj: " + obj.transform.position);
        Material material = new Material(Shader.Find("Standard"));
        Texture2D texture = Resources.Load<Texture2D>(libpath + "/books/" + bookname + "/page_" + bookpage);
        Debug.Log("texture: " + texture.width + " " + texture.height);
        material.mainTexture = texture;
        Debug.Log("material: " + material.mainTexture.width + " " + material.mainTexture.height);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = material;

        obj.transform.SetParent(book.transform);*/
    }
    void createFatherSpine(GameObject objct)
    {
        Vector3 objctcoords = objct.transform.position;
        float objctsize = objct.GetComponent<Renderer>().bounds.size.x;
        GameObject spine = new GameObject();
        spine.name = "spine_" + objct.name;
        objctcoords.x = objctcoords.x + objctsize / 2;
        spine.transform.position = objctcoords;
        spine.transform.SetParent(objct.transform.parent);
        objct.transform.SetParent(spine.transform);
        /*turnSpineAround(spine);
        if (toppage != 0)
        {
            Debug.Log("descendo essa porra");
            for (int i = 0; i < toppage; i = i + 2)
            {
                moveCertainPageVertical(i);
            }
        }
        */
    }

    private IEnumerator LoadImage(string filePath, Material material)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load texture: " + uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                material.mainTexture = texture;
                Debug.Log("Loaded texture: " + texture.width + "x" + texture.height);
            }
        }
    }
    void Start()
    {   //Levando em consideração que o livro já foi carregado.
        //LoadBook deve carregar TODAS as páginas do livro com uma pequena distância no eixo y
        LoadBook("morkborgjpgs");
        /*libpath = libpath.Replace("\\", "/");
        /*
        //pegue as páginas da pasta de livros, 
        //crie um objeto para cada página e adicione a imagem da página ao objeto
        //adicione o objeto ao corpo do livro
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(0.02f, 0.01f, 0.03f);
        Debug.Log("obj: " + obj.transform.position);
        Material material = new Material(Shader.Find("Standard"));

        Texture2D texture = Resources.Load<Texture2D>("books/book0/page_1");
        Debug.Log("texture: " + texture.width + " " + texture.height);
        material.mainTexture = texture;
        Debug.Log("material: " + material.mainTexture.width + " " + material.mainTexture.height);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = material;

        book.transform.SetParent(book.transform);*/
    }
}
