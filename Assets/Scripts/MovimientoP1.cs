using UnityEngine;
using System.Collections;

public class MovimientoP1 : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [Header("Movimiento")]
    public float speed = 5f;
    public float salto = 10f;
    public float dobleSalto = 8f;

    [Header("Dash Aéreo")]
    public float dashForce = 15f;     // fuerza del dash
    public float dashDuration = 0.2f; // cuánto dura el dash
    private bool canAirDash = true;   // se reinicia al tocar suelo
    private bool isDashing = false;

    private bool canDoubleJump = false;
    private bool hasDoubleJumpPowerUp = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing) // mientras dasheás, no podés mover
        {
            // Movimiento horizontal
            float moveHorizontal = Input.GetAxis("Horizontal");
            rigidbody2D.linearVelocity = new Vector2(moveHorizontal * speed, rigidbody2D.linearVelocity.y);
        }

        // Salto normal y doble salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                Jump(salto);
                canDoubleJump = true; // habilitamos el doble salto cuando dejamos el suelo
            }
            else if (canDoubleJump && hasDoubleJumpPowerUp)
            {
                Jump(dobleSalto);
                canDoubleJump = false; // ya usó su doble salto
            }
        }

        // Dash aéreo
        if (Input.GetKeyDown(KeyCode.RightShift) && !IsGrounded() && canAirDash)
        {
            StartCoroutine(DoAirDash());
        }
    }

    private void Jump(float fuerza)
    {
        Vector2 jumpForce = new Vector2(0, fuerza);
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0); // resetear Y antes del salto
        rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    // Detecta si tocamos el suelo
    private bool IsGrounded()
    {
        // Placeholder: ideal usar raycast o collision checks
        return Mathf.Abs(rigidbody2D.linearVelocity.y) < 0.01f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto"))
        {
            Debug.Log("PowerUp de doble salto obtenido");
            hasDoubleJumpPowerUp = true;
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            canDoubleJump = false; // resetea al tocar el suelo
            canAirDash = true;     // recuperamos el dash
        }
    }

    private IEnumerator DoAirDash()
    {
        isDashing = true;
        canAirDash = false;

        float direction = Input.GetAxisRaw("Horizontal"); 
        if (direction == 0) direction = transform.localScale.x; // si no apretás, dasha hacia donde mire

        // Aplica impulso horizontal, cancelando velocidad previa
        rigidbody2D.linearVelocity = new Vector2(0, 0);
        rigidbody2D.AddForce(new Vector2(direction * dashForce, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
