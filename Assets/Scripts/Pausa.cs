using UnityEngine;

public class Pausa : MonoBehaviour
{
    public void reanudarJuego()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void salirAlMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
