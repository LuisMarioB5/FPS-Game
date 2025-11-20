using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 15f; // Más lento que tus balas para poder esquivar
    public int damage = 10;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca con el Jugador
        if (other.CompareTag("Player"))
        {
            // Buscamos el script de salud (que ya hicimos genérico)
            HealthSystem hp = other.GetComponent<HealthSystem>();
            if (hp != null)
            {
                hp.TakeDamage(damage); // Daña al jugador
            }
            Destroy(gameObject);
        }
        // Si choca con el escenario (no con otros enemigos)
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}