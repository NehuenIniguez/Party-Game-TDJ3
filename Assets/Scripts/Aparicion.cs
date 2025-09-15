using UnityEngine;

public class Aparicion : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindAnyObjectByType<Desaparecer>().ActivarObjetos();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindAnyObjectByType<Desaparecer>().DesactivarObjetos();
        }
    }
}
