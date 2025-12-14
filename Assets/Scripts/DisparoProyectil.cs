using UnityEngine;

public class DisparoProyectil : MonoBehaviour
{
    public GameObject prefabPlataforma;
    public Transform puntoDisparo;
    //public float duracionPlataforma = 1f;
    public float tiempoEntreDisparos = 10f;

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
        Debug.Log("hola que tal chavales");


        // Destruir luego de X segundos
        //Destroy(plataforma, duracionPlataforma);
    }
}
