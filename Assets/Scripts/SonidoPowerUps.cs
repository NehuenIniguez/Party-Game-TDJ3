using System.Collections;
using UnityEngine;

public class SonidoPowerUps : MonoBehaviour
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
            StartCoroutine(reproducirSonido());
        }
    }
    IEnumerator reproducirSonido()
    {
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
