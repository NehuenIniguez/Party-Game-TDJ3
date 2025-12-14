using UnityEngine;

public class DestruirLluvia : MonoBehaviour
{
    //destruir despues de 1 segundo
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
