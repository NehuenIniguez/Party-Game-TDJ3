using UnityEngine;
using UnityEngine.Events;

public class PlatformDisappear : MonoBehaviour
{
    [Header("Evento que hace desaparecer la plataforma")]
    public UnityEvent onDisappear;

    private SpriteRenderer spriteRenderer;
   

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        // Si no tiene evento asignado en el inspector, lo inicializamos
        if (onDisappear == null)
            onDisappear = new UnityEvent();

        // Vinculamos el método para desaparecer al evento
        onDisappear.AddListener(Disappear);
    }

    /// <summary>
    /// Método que se ejecuta cuando se dispara el evento.
    /// Desactiva sprite y colisión.
    /// </summary>
    public void Disappear()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        
    }

    /// <summary>
    /// (Opcional) Método para volver a aparecer la plataforma
    /// </summary>
    public void Reappear()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

       
    }
}
