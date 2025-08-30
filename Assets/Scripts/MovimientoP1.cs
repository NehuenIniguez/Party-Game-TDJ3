using UnityEngine;
using System.Collections;

public class MovimientoP1 : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [Header("Movimiento")]
    public float speed = 5f;
    public float salto = 10f;
    public float orbJumpForce = 12f;

    [Header("Dash Aéreo")]
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    private bool canAirDash = true;
    private bool isDashing = false;

    private bool isInsideOrb = false;   // ¿estamos dentro de un círculo/orb?
    private Collider2D currentOrb = null;

    private Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDashing)
        {
            // Movimiento horizontal
            float moveHorizontal = Input.GetAxis("Horizontal");
            rigidbody2D.linearVelocity = new Vector2(moveHorizontal * speed, rigidbody2D.linearVelocity.y);
            animator.SetFloat("Caminata", Mathf.Abs(moveHorizontal));
            if (moveHorizontal > 0)
            { 
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if (moveHorizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        // Salto normal desde el suelo
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump(salto);
        }

        // Salto con ORB (Geometry Dash style)
        if (Input.GetKeyDown(KeyCode.Space) && isInsideOrb && currentOrb != null)
        {
            Jump(orbJumpForce);
            isInsideOrb = false;
            currentOrb = null;
        }

        // Dash aéreo
        if (Input.GetKeyDown(KeyCode.RightShift) && !IsGrounded() && canAirDash)
        {
            StartCoroutine(DoAirDash());
        }
    }

    private void Jump(float fuerza)
    {
        // Resetear velocidad Y antes de aplicar impulso
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0);
        rigidbody2D.AddForce(new Vector2(0, fuerza), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        // Placeholder, lo ideal es raycast o colisión con "Suelo"
        return Mathf.Abs(rigidbody2D.linearVelocity.y) < 0.01f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto")) // orb
        {
            Debug.Log("Entró en orb");
            isInsideOrb = true;
            currentOrb = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto"))
        {
            Debug.Log("Salió del orb");
            isInsideOrb = false;
            currentOrb = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            canAirDash = true; // recuperamos el dash al tocar suelo
            animator.SetBool("Dash", false);
        }
    }

    private IEnumerator DoAirDash()
    {
        isDashing = true;
        canAirDash = false;

        float direction = Input.GetAxisRaw("Horizontal");
        if (direction == 0) direction = transform.localScale.x;

        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.AddForce(new Vector2(direction * dashForce, 0), ForceMode2D.Impulse);
        animator.SetBool("Dash", true);

        yield return new WaitForSeconds(dashDuration);
        
        isDashing = false;
    }
}
