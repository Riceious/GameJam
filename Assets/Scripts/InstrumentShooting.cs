using UnityEngine;

public class InstrumentShooting : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackCooldown = 0.5f;

    private float cooldownTimer = 0f;
    private PlayerMovement playerMovement;
    private Camera mainCamera;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mainCamera = Camera.main;

        // Crear punto de disparo si no existe
        if (firePoint == null)
        {
            GameObject firePointObj = new GameObject("FirePoint");
            firePoint = firePointObj.transform;
            firePoint.SetParent(transform);
            firePoint.localPosition = Vector3.zero;
        }
    }

    void Update()
    {
        // Controlar cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Disparar con click izquierdo
        if (Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            Shoot();
            cooldownTimer = attackCooldown;
        }

        // Actualizar posición del firePoint basado en mouse
        UpdateFirePointPosition();
    }

    void UpdateFirePointPosition()
    {
        // Obtener posición del mouse en mundo 2D
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Calcular dirección hacia el mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Posicionar firePoint a cierta distancia del jugador
        float offsetDistance = 0.5f;
        firePoint.position = (Vector2)transform.position + direction * offsetDistance;
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            // Instanciar proyectil
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Obtener dirección hacia el mouse
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

            // Configurar proyectil
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * projectileSpeed;
            }

            // Rotar proyectil hacia la dirección
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Destruir proyectil después de tiempo
            Destroy(projectile, 3f);
        }
    }
}