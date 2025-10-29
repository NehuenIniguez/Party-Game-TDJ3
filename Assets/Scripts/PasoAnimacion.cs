using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; 

public class PasoAnimacion : MonoBehaviour
{
    public float tiempoEspera = 5f; // Tiempo en segundos antes de cambiar de escena

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= tiempoEspera)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
