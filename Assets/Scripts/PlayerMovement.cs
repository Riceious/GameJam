using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input del teclado
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizar para movimiento diagonal
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Guardar �ltima direcci�n para animaciones y disparo
        if (movement != Vector2.zero)
        {
            lastDirection = movement;
        }

        // Actualizar animaciones si tienes Animator
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Movimiento f�sico
        rb.velocity = movement * moveSpeed;
    }

    void UpdateAnimations()
    {
        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("LastHorizontal", lastDirection.x);
            animator.SetFloat("LastVertical", lastDirection.y);
        }
    }

    // M�todo p�blico para obtener la direcci�n
    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}