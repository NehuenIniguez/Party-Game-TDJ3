using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{ 
public Transform puntoA;
    public Transform puntoB;
    public float duracion = 2f;

    private float t = 0f;
    private Transform jugadorArriba = null;

    void Update()
    {
        t += Time.deltaTime / duracion;
        float pingPong = Mathf.PingPong(t, 1);

        Vector3 posicionAnterior = transform.position;
        transform.position = Vector3.Lerp(puntoA.position, puntoB.position, pingPong);

        // Si el jugador está arriba, moverlo la misma diferencia
        if (jugadorArriba != null)
        {
            Vector3 diferencia = transform.position - posicionAnterior;
            jugadorArriba.position += diferencia;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorArriba = collision.collider.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorArriba = null;
        }
    }
}
