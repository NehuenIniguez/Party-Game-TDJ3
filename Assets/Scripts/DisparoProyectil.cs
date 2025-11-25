using UnityEngine;

public class DisparoProyectil : MonoBehaviour
{
    public GameObject prefabPlataforma;
    public Transform puntoDisparo;
    public float velocidadPlataforma = 5f;
    public float duracionPlataforma = 3f;
    public float tiempoEntreDisparos = 1f;

    private float tiempoUltimoDisparo = 0f;

    void Update()
    {
        tiempoUltimoDisparo += Time.deltaTime;

        // Acá ponés la tecla si querés (por ejemplo: Input.GetKeyDown(KeyCode.X))
        if (tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            DispararPlataforma();
            tiempoUltimoDisparo = 0f;
        }
    }

    void DispararPlataforma()
    {
        // Instanciar la plataforma
        GameObject plataforma = Instantiate(prefabPlataforma, puntoDisparo.position, Quaternion.identity);

        // Obtener Rigidbody2D
        Rigidbody2D rb = plataforma.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Velocity correcto (linearVelocity no existe)
            rb.linearVelocity = -transform.right * velocidadPlataforma;
        }

        // Destruir luego de X segundos
        Destroy(plataforma, duracionPlataforma);
    }
}
