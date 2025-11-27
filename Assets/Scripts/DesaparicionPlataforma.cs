using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DesaparicionPlataforma : MonoBehaviour
{ 
    public float tiempoDesaparicion = 2f;
    private bool activado = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activado && collision.collider.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(Desaparecer());
        }
    }

    private IEnumerator Desaparecer()
    {
        // Espera el tiempo configurado
        yield return new WaitForSeconds(tiempoDesaparicion);


        // Opción B (alternativa): desactivar sin destruir
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
