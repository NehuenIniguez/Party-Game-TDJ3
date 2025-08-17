using UnityEngine;

public class MovimientoP1 : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private bool move = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidbody2D.linearVelocity = new Vector2(0, 10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody2D.linearVelocity = new Vector2(10, 0);
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidbody2D.linearVelocity = new Vector2(-10, 0);
            move = true;
        }
        if (Input.GetKeyUp(KeyCode.D)&& move || Input.GetKeyUp(KeyCode.A)&& move)
        {
            rigidbody2D.linearVelocity = new Vector2(0, 0);
            move = false;
        }
    }
}
