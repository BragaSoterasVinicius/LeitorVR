using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class animacaodeabrirqndosegurar : MonoBehaviour
{
    public Animator animator;
    public InputActionReference abrirLivroAction;
    private void Awake()
    {
        abrirLivroAction.action.Enable();
        abrirLivroAction.action.performed += AbrirLivro;
    }

    private void OnDestroy()
    {
        abrirLivroAction.action.Disable();
        abrirLivroAction.action.performed -= AbrirLivro;
    }

    private void AbrirLivro(InputAction.CallbackContext context)
    {
        animator.SetTrigger("abrir");
    }
}
