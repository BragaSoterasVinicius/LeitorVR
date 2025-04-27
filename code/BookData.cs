using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookData : MonoBehaviour
{
    public int bookID;
    public string bookPath;
    public int bookPosition;

    public BookData(int bookID, string bookPath, int bookPosition = 0)
    {
        this.bookID = bookID;
        this.bookPath = bookPath;
        this.bookPosition = bookPosition;
    }
}
