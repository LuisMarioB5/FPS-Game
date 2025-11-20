using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public float speed = 80f;
    public float lifeTime = 3f;
    
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (hasHit) return;

        float moveDistance = speed * Time.deltaTime;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, moveDistance))
        {
            HandleImpact(hit.collider);
            GameManager.instance.GastarBala();
        }
        else
        {
            transform.Translate(Vector3.forward * moveDistance);
        }
    }

    void HandleImpact(Collider other)
    {
        if (hasHit) return;
        if (other.CompareTag("Player")) return;

        hasHit = true;

        HealthSystem enemigo = other.GetComponent<HealthSystem>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño(1);
        }

        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            zone.HandleHit();
        }

        Destroy(gameObject);
    }
}