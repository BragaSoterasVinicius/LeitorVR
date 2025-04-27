using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pararOuComeçarMusica : MonoBehaviour
{
    public bool IsMusicaTocando = true;
    public GameObject audioSource;
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            if (IsMusicaTocando)
            {
                audioSource.GetComponent<AudioSource>().Stop();
                IsMusicaTocando = false;
            }
            else
            {
                audioSource.GetComponent<AudioSource>().Play();
                IsMusicaTocando = true;
            }
        }
    }
}
