using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasoNivel : MonoBehaviour
{
  private AudioSource audioSource;
  public AudioClip audioClip;
    void Start()
    {
    audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      StartCoroutine(PasoNivelCoroutine());
    }
  }

  public void pasonivel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  IEnumerator PasoNivelCoroutine()
  { 
    gameObject.GetComponent<Animator>().SetTrigger("Recolectado");
    audioSource.PlayOneShot(audioClip);
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
