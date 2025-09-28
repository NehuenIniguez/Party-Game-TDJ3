using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestruir : MonoBehaviour
{
    private static NoDestruir instancia;

     void Awake()
    {
        if (instancia != null && instancia != this)
        {
            //Destroy(gameObject);
        }
        else
        {
            instancia = this;
            //DontDestroyOnLoad(gameObject);

            // 👇 Suscribirse al evento de cambio de escena
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        // 👇 Desuscribirse cuando se destruye (importante para evitar errores si se recarga la escena)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método para manejar el evento sceneLoaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Puedes dejarlo vacío o agregar lógica si es necesario
    }
}
