using UnityEngine;

public class DisparoPlataforma : MonoBehaviour
{
    public GameObject prefabPlataforma;
    public Transform puntoDisparo;
    public float velocidadPlataforma = 5f;
    public float duracionPlataforma = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // tecla para disparar plataforma
        {
            DispararPlataforma();
        }
    }

    void DispararPlataforma()
    {
        // Instanciamos la plataforma
        GameObject plataforma = Instantiate(prefabPlataforma, puntoDisparo.position, Quaternion.identity);

        // Hacemos que se mueva como un disparo
        Rigidbody2D rb = plataforma.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // no cae
        rb.linearVelocity = transform.right * velocidadPlataforma; // se mueve hacia adelante

        // La destruimos después de X segundos
        Destroy(plataforma, duracionPlataforma);
}
}