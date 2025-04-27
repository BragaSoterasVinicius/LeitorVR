using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressableButton : MonoBehaviour
{
    public UnityEvent onPressed;
    //public UnityEvent onReleased;

    private bool isPressed = false;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu caralho");
        if (!isPressed && other.CompareTag("PlayerHand")) // Tag the hand/controller as "PlayerHand"
        {
            Debug.Log("PlayerHand Tocada -> Mudança de Y iminente");
            isPressed = true;
            //transform de teste (só pra ver se muda quando tocado)
            //transform.position -= new Vector3(0.766f, 0.1f, 0);
            onPressed?.Invoke();
        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Saiu");
        if (isPressed && other.CompareTag("PlayerHand"))
        {
            isPressed = false;
            transform.position += new Vector3(0.766f, 0.1f, 0);
            onReleased?.Invoke();
        }
    }*/
}
