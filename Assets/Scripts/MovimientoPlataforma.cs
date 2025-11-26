using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{ 
    public Transform puntoA;
    public Transform puntoB;
    public float duracion = 2f;

    private float t = 0f;

    void Update()
    {
        t += Time.deltaTime / duracion;

        // movimiento ida/vuelta con onda senoidal
        float pingPong = Mathf.PingPong(t, 1);

        transform.position = Vector3.Lerp(puntoA.position, puntoB.position, pingPong);
    }
}
