using UnityEngine;
using System.Collections;

public class MovimientoP1 : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [SerializeField] private Transform respawnPoint;

    [Header("Movimiento")]
    public float speed = 5f;
    public float salto = 10f;
    public float orbJumpForce = 12f;

    [Header("Dash Aéreo")]
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    private bool canAirDash = true;
    private bool isDashing = false;

    private bool isInsideOrb = false;   
    private Collider2D currentOrb = null;

    private Animator animator;

    private int facingDirection = 1; // 1 = derecha, -1 = izquierda

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
                facingDirection = 1;
            }
            else if (moveHorizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                facingDirection = -1;
            }
        }

        // Salto normal
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump(salto);
        }

        // Salto con ORB
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
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0);
        rigidbody2D.AddForce(new Vector2(0, fuerza), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Mathf.Abs(rigidbody2D.linearVelocity.y) < 0.01f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto")) 
        {
            isInsideOrb = true;
            currentOrb = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto"))
        {
            isInsideOrb = false;
            currentOrb = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            canAirDash = true;
            animator.SetBool("Dash", false);
        }

        if (collision.gameObject.CompareTag("Caida"))
        {
            transform.position = respawnPoint.position; 
        }
    }

    private IEnumerator DoAirDash()
    {
        isDashing = true;
        canAirDash = false;

        // Mantener la velocidad vertical, solo modificar la horizontal
        rigidbody2D.linearVelocity = new Vector2(facingDirection * dashForce, rigidbody2D.linearVelocity.y);

        animator.SetBool("Dash", true);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
