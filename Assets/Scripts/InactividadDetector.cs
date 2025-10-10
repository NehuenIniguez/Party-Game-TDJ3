using UnityEngine;
using TMPro;

public class InactividadDetector : MonoBehaviour
{
    [Header("Referencias")]
    public Canvas noSeMueveCanvas;
    public TextMeshProUGUI[] mensajes;

    [Header("Tiempos")]
    public float tiempoInactividad = 3f; // Tiempo sin moverse antes de mostrar mensaje
    public float duracionMensaje = 2f;   // Duración de cada mensaje

    private int mensajeActual = 0;
    private float tiempoSinMoverse = 0f;
    private Vector3 ultimaPosicion;
    private bool mostrandoMensaje = false;

    void Start()
    {
        ultimaPosicion = transform.position;
        noSeMueveCanvas.gameObject.SetActive(false);

        // Desactivamos todos los textos
        foreach (var t in mensajes)
            t.gameObject.SetActive(false);
    }

    void Update()
    {
        // Detectar si el jugador se movió
        if (Vector3.Distance(transform.position, ultimaPosicion) < 0.01f)
        {
            tiempoSinMoverse += Time.deltaTime;
        }
        else
        {
            tiempoSinMoverse = 0f;
        }

        ultimaPosicion = transform.position;

        // Mostrar siguiente mensaje si está quieto y no se está mostrando otro
        if (tiempoSinMoverse >= tiempoInactividad && !mostrandoMensaje && mensajeActual < mensajes.Length)
        {
            StartCoroutine(MostrarMensaje());
        }
    }

    System.Collections.IEnumerator MostrarMensaje()
    {
        mostrandoMensaje = true;

        noSeMueveCanvas.gameObject.SetActive(true);
        mensajes[mensajeActual].gameObject.SetActive(true);

        yield return new WaitForSeconds(duracionMensaje);

        mensajes[mensajeActual].gameObject.SetActive(false);
        noSeMueveCanvas.gameObject.SetActive(false);

        mensajeActual++;
        tiempoSinMoverse = 0f;
        mostrandoMensaje = false;

        // Si ya se mostraron todos los mensajes, cerramos el juego
        if (mensajeActual >= mensajes.Length)
        {
            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
    }
}
