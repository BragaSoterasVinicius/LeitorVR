using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTheBookRelativeToTheBucheregal : MonoBehaviour
{
    public void moveBookToEstante(GameObject book, GameObject estante)
    {
        //além do livro ter que ficar em cima da estante, ele deve ficar em uma posição específica
        //que depende de quantos livros já estão colocados na estante
        //vale a pena primeiro usar no json o id dos livros para decidir sua posição na estante

        //pega o id do livro (vou ter q fazer o sistema de save and load funfar ainda)

        //transforma o id em uma posição em relação a estante
        //Vector3 bookRelativePosition = new Vector3(posiçãohorizontalnaestante, alturadolivro, nseiaindamasvaiserprofundidade);
        //book.transform.position = estante.transform.position + bookRelativePosition;
        //posso fazer um negocio parecido com skyrim (Ich werde mich erinnern)
    }
}
