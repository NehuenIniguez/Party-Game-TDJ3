using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestruir : MonoBehaviour
{
    private static NoDestruir instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // Destruye duplicados
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Esto mantiene la música entre escenas

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Lógica opcional (por ejemplo, bajar el volumen en el menú)
    }
}
