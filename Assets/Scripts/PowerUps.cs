using UnityEngine;

public class PowerUps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Libro"))
        {
            GetComponent<DisparoPlataforma>().enabled = true;
            
        }
        if (collision.CompareTag("Pluma"))
        {
            GetComponent<AirDash>().enabled = true;
            
        }
    }
}
