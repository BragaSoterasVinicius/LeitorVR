using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveAndLoadBookSys : MonoBehaviour
{
    public static int loadBookIndex()
    {
        string path = Application.persistentDataPath + "/bookIndex.vrbook";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)bf.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 0;
        }
    }
    public static string saveBookIndex(int index)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/bookIndex.vrbook";
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, index);
        stream.Close();
        return "index saved!";
    }
    public static string saveBookData(int bookID, string bookPath, int bookPosition = 0)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/bookData" + bookID + ".vrbook";
        FileStream stream = new FileStream(path, FileMode.Create);

        BookData data = new BookData(bookID, bookPath);

        bf.Serialize(stream, data);
        stream.Close();
        return "book saved!";
    }

    public static BookData loadBook(int bookID)
    {
        string path = Application.persistentDataPath + "/bookData" + bookID + ".vrbook";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BookData data = bf.Deserialize(stream) as BookData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
