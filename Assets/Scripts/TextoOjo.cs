using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using TMPro;

public class ControladorTutorial : MonoBehaviour
{
    public GameObject[] textos;
    public Image imagenFondo;

    private int indice = 0;
    [SerializeField] private CinemachineCamera cinemachine;
    private TextMeshProUGUI textoActualUI;
    private string textoCompleto;
    private Coroutine escribiendoTexto;

    [Header("Configuración")]
    public float velocidadCaracter = 0.02f; // tiempo entre letras
    public float tiempoEntreTextos = 2f;    // pausa después de terminar cada texto

    void Start()
    {
        Time.timeScale = 0; // congela el juego

        // Activa fondo y primer texto
        imagenFondo.enabled = true;
        
        for (int i = 0; i < textos.Length; i++)
            textos[i].SetActive(i == 0);

        if (textos.Length > 0)
        {
            textoActualUI = textos[0].GetComponentInChildren<TextMeshProUGUI>();
            if (textoActualUI != null)
            {
                textoCompleto = textoActualUI.text;
                textoActualUI.text = "";
                escribiendoTexto = StartCoroutine(MostrarCaracter());
            }
        }

        // 🔑 Forzar que todos los animators usen tiempo no escalado
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator anim in animators)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    void AvanzarTexto()
    {
        textos[indice].SetActive(false);
        indice++;

        if (indice < textos.Length)
        {
            textos[indice].SetActive(true);

            textoActualUI = textos[indice].GetComponentInChildren<TextMeshProUGUI>();
            if (textoActualUI != null)
            {
                textoCompleto = textoActualUI.text;
                textoActualUI.text = "";
                escribiendoTexto = StartCoroutine(MostrarCaracter());
            }
            else
            {
                Debug.LogError("No se encontró componente TextMeshProUGUI en: " + textos[indice].name);
            }
        }
        else
        {
            // Fin del tutorial
            if (cinemachine != null)
                cinemachine.Lens.OrthographicSize = 18f;

            Time.timeScale = 1;
            imagenFondo.enabled = false;
            //Destroy(gameObject);
        }
    }

    IEnumerator MostrarCaracter()
    {
        foreach (char c in textoCompleto)
        {
            textoActualUI.text += c;
            yield return new WaitForSecondsRealtime(velocidadCaracter);
        }

        escribiendoTexto = null;

        // Esperar un tiempo antes de pasar al siguiente texto
        yield return new WaitForSecondsRealtime(tiempoEntreTextos);

        AvanzarTexto();
    }
}
