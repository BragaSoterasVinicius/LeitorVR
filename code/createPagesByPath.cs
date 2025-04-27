using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System;
public class createPagesByPath : MonoBehaviour
{
    public GameObject book;
    public float distanceAmongPages;

    public string libpathexternal;

    public Vector3 vectorDeSummon;

    private RenderPages renderPages;
    //public GameObject debogaReader;
    //Esse método aqui de baixo deve carregar as texturas de um intervalo de páginas
    //de um livro que já foi carregado

    //esse metodo tem que carregar um objeto *novo* cada vez que é chamado
    public void LoadBook(string libpath, GameObject debugReader, GameObject bookoriginal, float distanceAmongPages = 0.01f)
    {
        GameObject book = Instantiate(bookoriginal);
        book.transform.position = vectorDeSummon;
        libpath = libpath.Replace("\\", "/");
        Rigidbody rb = book.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true; // Enable gravity
        }
        int TotalFolderFiles = Directory.GetFiles(libpath).Length;
        GameObject lastPage = null;
        Vector3 parentPosition = book.transform.position;
        //coordenadas de cria para criar um collider com o tamanho do livro e de centro bem no meio do livro
        book.GetComponent<BoxCollider>().size = new Vector3(0.20923f, distanceAmongPages * (float)TotalFolderFiles * 0.01f, 0.29f);
        book.GetComponent<BoxCollider>().center = new Vector3(0f, -distanceAmongPages * (float)TotalFolderFiles * 0.01f / 2f, 0f);
        //pegue as páginas da pasta de livros, 
        //crie um objeto para cada página e adicione a imagem da página ao objeto
        //adicione o objeto ao corpo do livro

        //importante lembrar que cada folha tem duas páginas, então, por i, vai haver uma página normal e a seguinte ainda vai estar
        // na mesma coordenada y, apenas rodando 180 graus no eixo z
        //esse aqui cria as páginas em branco
        for (int i = 0; i < TotalFolderFiles; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.name = "page_" + i;
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

            /* agora trocou esse trecho pelo novo método LoadTexturesForThePage
            Material material = new Material(Shader.Find("Standard"));
            //ATENCAO: ESSA LINHA ABAIXO SÓ PEGA OS ARQUIVOS .JPG, SE FOR USAR OUTRO FORMATO, MUDE AQUI
            string imagePath = (libpath + "/page_" + i + ".png");
            StartCoroutine(LoadImage(imagePath, material, obj, debugReader));
*/
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
            libpathexternal = libpath;
            debugReader.GetComponent<TextMeshPro>().text = "external path loaded to be saved: " + libpath;
            /*
            obj.transform.SetParent(book.transform);*/
        }

        //aqui é importante só carregar as primeiras páginas então vou fazer um calculinho para tal
        if (calculateIfCutNecessary(TotalFolderFiles))
        {
            renderPages.LoadTexturesForThePage(0, 0, libpath, book, debugReader);
            renderPages.LoadTexturesForThePage(TotalFolderFiles - 2, TotalFolderFiles - 1, libpath, book, debugReader);
            book.GetComponent<TurnPages>().LastPageRendered = 0;
            //agora só renderizar a última página pra ficar mais bonitinho
        }
        else
        {
            renderPages.LoadTexturesForThePage(0, TotalFolderFiles, libpath, book, debugReader);
            book.GetComponent<TurnPages>().LastPageRendered = TotalFolderFiles;
        }
        book.GetComponent<TurnPages>().TotalPages = TotalFolderFiles;
        book.GetComponent<TurnPages>().path = libpath;
        //rb.useGravity = true;


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

    public bool calculateIfCutNecessary(int TotalFolderFiles)
    {
        if (TotalFolderFiles > 40)
        {
            return true;
        }
        return false;
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
    public void UnloadBookTextures(GameObject book, GameObject debugReader)
    {
        int totalPages = book.GetComponent<TurnPages>().TotalPages;
        renderPages.UnloadTexturesForThePage(0, totalPages, book, debugReader);
    }
    void Start()
    {
        renderPages = GetComponent<RenderPages>();
        //Levando em consideração que o livro já foi carregado.
        //LoadBook deve carregar TODAS as páginas do livro com uma pequena distância no eixo y
        //LoadBook(@"C:\Users\Pichau\Documents\pedeefeparapenege\books\morkborgjpgs");
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
