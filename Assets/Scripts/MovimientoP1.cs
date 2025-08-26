using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MovimientoP1 : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float speed = 5f;
    public float salto = 10f;
    public float dobleSalto = 20f;
    private bool canDoubleJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rigidbody2D.linearVelocityX = moveHorizontal * speed;
        if (Input.GetKeyDown(KeyCode.Space) && !canDoubleJump)
        {
            Vector2 jump = new Vector2(0, salto);
            rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
            canDoubleJump = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DobleSalto") && Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            Debug.Log("dooble salto activo");
            Vector2 doblejump = new Vector2(0, dobleSalto);
            rigidbody2D.AddForce(doblejump, ForceMode2D.Impulse);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("toco suelo");
            canDoubleJump = false;
        }
    }
}
