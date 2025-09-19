using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformasAnimacion : MonoBehaviour
{
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        StartCoroutine(Pisadas());
    }
}
    IEnumerator Pisadas()
    { 
        GetComponent<Animator>().SetTrigger("Pisada");
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetTrigger("Subir");
    }
}
