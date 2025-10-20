using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Da�o del Proyectil")]
    public int damage = 10;

    [Header("Efectos Visuales")]
    public GameObject impactEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // No colisionar con el jugador
        if (collision.CompareTag("Player"))
            return;

        // Aqu� puedes agregar l�gica para da�ar enemigos
        if (collision.CompareTag("Enemy"))
        {
            // Ejemplo: collision.GetComponent<ShadowEnemy>().TakeDamage(damage);
        }










        // Efecto de impacto
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        // Destruir proyectil
        Destroy(gameObject);
    }
}