using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrashBin : MonoBehaviour
{
    public AudioSource acceptSoundSource; 
    public AudioSource errorSoundSource;
    public string trashBinColor;
    private void OnCollisionEnter(Collision collision)
    {  
        if (collision.gameObject.CompareTag("Trash"))
        {
            Trash trashScript = collision.gameObject.GetComponent<Trash>();
            if (trashScript != null)
            {
                if (trashScript.belongsToTrashBinTag == trashBinColor)
                {
                    Debug.Log("Lixo correto");
                    acceptSoundSource.Play();
                    Destroy(collision.gameObject);
                    StartCoroutine(JumpAnimation());
                }
                else
                {
                    Debug.Log("Lixo errado");
                    errorSoundSource.Play(); 
                    Destroy(collision.gameObject);
                    StartCoroutine(ShakeAnimation());
                }
            }
        }
    }

    IEnumerator ShakeAnimation()
    {
        Vector3 originalPosition = transform.position;
        float duration = 1f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.position = originalPosition + Random.insideUnitSphere * 0.3f; // O '0.1f' é a amplitude do tremor
            yield return null;
        }

        transform.position = originalPosition;
    }

    IEnumerator JumpAnimation()
    {
        Vector3 originalPosition = transform.position;
        float jumpHeight = 0.5f;
        float duration = 0.5f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float fraction = t / duration;
            transform.position = originalPosition + Vector3.up * Mathf.Sin(fraction * Mathf.PI) * jumpHeight;
            yield return null;
        }

        transform.position = originalPosition;
    }
}


