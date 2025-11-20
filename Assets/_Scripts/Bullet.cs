using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public float speed = 80f;
    public float lifeTime = 3f;
    
    [Header("Efectos Visuales")]
    [SerializeField] private GameObject impactVfxPrefab;
    [SerializeField] private float vfxDuration = 2f;

    [Header("Daño a Enemigos")]
    [SerializeField] private int damage = 1;

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
            HandleImpact(hit);
            GameManager.instance.GastarBala();
        }
        else
        {
            transform.Translate(Vector3.forward * moveDistance);
        }
    }

    void HandleImpact(RaycastHit hitInfo)
    {
        if (hasHit) return;
        
        if (hitInfo.collider.CompareTag("Player")) return; 

        hasHit = true;

        if (impactVfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(impactVfxPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            
            Destroy(vfxInstance, vfxDuration);
        }

        HealthSystem enemigo = hitInfo.collider.GetComponent<HealthSystem>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño(damage);
        }

        TargetZone zone = hitInfo.collider.GetComponent<TargetZone>();
        if (zone != null)
        {
            zone.HandleHit();
        }

        Destroy(gameObject);
    }
}