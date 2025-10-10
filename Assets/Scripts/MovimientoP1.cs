using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;
using Unity.Cinemachine;

public class MovimientoP1 : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip muerteSound;
    private AudioSource audioSource;
    public bool IsGrounded = false;
    [SerializeField] private CinemachineCamera camara;

    [Header("Movimiento")]
    public float speed = 5f;
    public float salto = 10f;
    public float orbJumpForce = 12f;

    private bool isInsideOrb = false;
    private Collider2D currentOrb = null;

    private Animator animator;

    [HideInInspector] public int facingDirection = 1; // 1 = derecha, -1 = izquierda

    private bool canMove = true;

    void Start()
    {
        Time.timeScale = 1; // Asegura que el tiempo esté normal al iniciar
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!canMove)
        {
            // Frena el movimiento horizontal, pero deja la velocidad vertical (por si está cayendo)
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            animator.SetFloat("Caminata", 0);
            return;
        }

        // Movimiento horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(moveHorizontal * speed, rb2D.linearVelocity.y);

        if (IsGrounded)
        {
            animator.SetFloat("Caminata", Mathf.Abs(moveHorizontal));
        }
        else
        {
            animator.SetFloat("Caminata", 0); // evita caminar en el aire
        }

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

        // Salto normal
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded || Input.GetButtonDown("Jump") && IsGrounded)
        {
            Jump(salto);
            animator.SetTrigger("Salto");
            Debug.Log(Mathf.Abs(rb2D.linearVelocity.y) < 0.01f);
            Debug.Log("la primera se desespera");
        }

        // Salto con ORB
        if (Input.GetKeyDown(KeyCode.Space) && isInsideOrb && currentOrb != null|| Input.GetButtonDown("Jump") && isInsideOrb && currentOrb != null)
        {
            Jump(orbJumpForce);
            isInsideOrb = false;
            currentOrb = null;
            animator.SetTrigger("DobleSalto");
        }
    }

    private void Jump(float fuerza)
    {
        audioSource.PlayOneShot(jumpSound);
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0);
        rb2D.AddForce(new Vector2(0, fuerza), ForceMode2D.Impulse);
    }

    //public bool IsGrounded()
    //{
    //    //
    //    //Debug.Log(Mathf.Abs(rb2D.linearVelocity.y) < 0.01f);
    //    
    //    //return Mathf.Abs(rb2D.linearVelocity.y) < 0.01f;
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DobleSalto"))
        {
            isInsideOrb = true;
            currentOrb = collision;
        }
        if (collision.CompareTag("Estrella"))
        {
            StartCoroutine(aniamcionGanar());
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
        if (collision.gameObject.CompareTag("Caida"))
        {
            StartCoroutine(aniamcionPerder());
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            animator.SetTrigger("Piso");
            IsGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            IsGrounded = false;
            animator.SetFloat("VelocidadY", rb2D.linearVelocity.y);
        }
    }

    IEnumerator aniamcionGanar()
    {
        canMove = false;
        animator.SetTrigger("Ganar");

        // Guardamos los valores actuales
        Transform objetivoOriginal = camara.Follow;
        float tamañoOriginal = camara.Lens.OrthographicSize;

        // Cambiamos el objetivo temporalmente al personaje
        camara.Follow = transform;

        // Offset opcional (por si tu personaje está más abajo o arriba del centro visual)
        Vector3 offset = new Vector3(0, 0, -10f); // mantené la Z de la cámara original
        
        Vector3 posicionObjetivo = transform.position;

        // Hacemos un zoom suave hacia el personaje
        float tamañoFinal = 5f;
        float duracionZoom = 0.5f;
        float tiempo = 0f;

        while (tiempo < duracionZoom)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, tiempo / duracionZoom); // más suave que Lerp lineal

            camara.Lens.OrthographicSize = Mathf.Lerp(tamañoOriginal, tamañoFinal, t);
            camara.transform.position = Vector3.Lerp(camara.transform.position, posicionObjetivo, t);

            yield return null;
        }

        camara.Lens.OrthographicSize = tamañoFinal;
        camara.transform.position = posicionObjetivo;

        // Espera mientras se reproduce la animación
        yield return new WaitForSeconds(2f);

    }
    IEnumerator aniamcionPerder()
    {
        audioSource.PlayOneShot(muerteSound);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(muerteSound.length);
        animator.SetTrigger("Caida");
        transform.position = respawnPoint.position;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}