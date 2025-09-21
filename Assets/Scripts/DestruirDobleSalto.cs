using System.Collections;
using UnityEngine;

public class DestruirDobleSalto : MonoBehaviour
{
    private Animator animator;
    private bool playerOnPlatform = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        animator.SetTrigger("Destruccion");
        yield return new WaitForSeconds(0.5f);
        Apagar();
    }
    private void Apagar()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        animator.SetTrigger("Normal");
    }
    
}
