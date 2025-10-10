using UnityEngine;
using System.Collections;

public class AirDash : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private MovimientoP1 movimiento;
    private Animator animator;
    public GameObject Pj;
    [HideInInspector] int facingDirection = 1; // 1 = derecha, -1 = izquierda

    [Header("Dash Aéreo")]
    public float dashForce = 35f;
    public float dashDuration = 0.2f;
    

    void Start()
    {
        Pj = gameObject.GetComponent<Rigidbody2D>().gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        movimiento = GetComponent<MovimientoP1>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && !movimiento.IsGrounded || Input.GetButtonDown("Fire2") && !movimiento.IsGrounded)
        {
            facingDirection = movimiento.facingDirection;
            StartCoroutine(DoAirDash());
        }
    }

    private IEnumerator DoAirDash()
    {
        // aplicar velocidad horizontal del dash
        rb2D.linearVelocity = new Vector2(facingDirection * dashForce, rb2D.linearVelocity.y);
        rb2D.AddForce(new Vector2(facingDirection * dashForce, 0), ForceMode2D.Impulse);
        

        animator.SetBool("Dash", true);

        yield return new WaitForSeconds(dashDuration);

        
        animator.SetBool("Dash", false);
    }
}
