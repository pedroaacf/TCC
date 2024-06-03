using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handTransform; // Posição da mão do jogador
    public KeyCode pickupKey = KeyCode.E; // Tecla para pegar/descartar objeto

    private bool heldObject = false; // Objeto atualmente segurado pelo jogador

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (!heldObject) // Se o jogador não estiver segurando nada
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Player"))
                    {
                        heldObject = true;
                        GetComponent<Rigidbody>().isKinematic = true; // Impede a física enquanto estiver segurando
                        transform.SetParent(handTransform); // Coloca o objeto na mão do jogador
                        transform.localPosition = Vector3.zero; // Reposiciona o objeto na mão do jogador
                        break;
                    }
                }
            } else // Se o jogador estiver segurando algo, largue o objeto
            {
                GetComponent<Rigidbody>().isKinematic = false; // Ativa a física do objeto
                transform.SetParent(null); // Libera o objeto da mão do jogador
                heldObject = false; // Reinicia a variável do objeto segurado
            }
        }
    }
}
