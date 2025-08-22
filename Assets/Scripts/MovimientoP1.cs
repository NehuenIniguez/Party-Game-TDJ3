using UnityEngine;

public class MovimientoP1 : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float speed = 5f;
    public float salto = 5f;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector2 jump = new Vector2(0, salto);
            rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
        }
    }
}
