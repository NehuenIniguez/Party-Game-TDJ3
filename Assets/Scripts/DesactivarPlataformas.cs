using UnityEngine;

public class DesactivarPlataformas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<PlatformDisappear>().onDisappear.Invoke();
        }
    }
}
