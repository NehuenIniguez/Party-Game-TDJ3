using UnityEngine;
using System.Collections;

public class AirDash : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private MovimientoP1 movimiento;
    private Animator animator;

    [Header("Dash Aéreo")]
    public float dashForce = 30f;
    public float dashDuration = 0.25f;

    private bool isDashing = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        movimiento = GetComponent<MovimientoP1>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Si presiona el botón de dash y está en el aire, inicia el dash
        if ((Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire2")) 
            && !movimiento.IsGrounded() 
            && !isDashing)
        {
            StartCoroutine(DoAirDash());
        }
    }

    private IEnumerator DoAirDash()
    {
        isDashing = true;

        // Bloqueamos el movimiento del script principal mientras dura el dash
        movimiento.enabled = false;

        // Activamos la animación
        animator.SetBool("Dash", true);

        // Guardamos la gravedad original para suspenderla durante el dash
        float gravedadOriginal = rb2D.gravityScale;
        rb2D.gravityScale = 0f;

        // Aplicamos velocidad constante en la dirección que mira el personaje
        rb2D.linearVelocity = new Vector2(movimiento.facingDirection * dashForce, 0f);

        // Esperamos el tiempo del dash
        yield return new WaitForSeconds(dashDuration);

        // Restauramos todo
        rb2D.gravityScale = gravedadOriginal;
        animator.SetBool("Dash", false);
        movimiento.enabled = true;
        isDashing = false;
    }
}
