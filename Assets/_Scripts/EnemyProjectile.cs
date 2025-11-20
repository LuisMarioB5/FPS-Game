using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 10;
    public float lifeTime = 5f;

    [Header("Efectos")]
    public GameObject impactVfxPrefab;
    public float vfxDuration = 2f;

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
        if (impactVfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(impactVfxPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180, 0));
            Destroy(vfxInstance, vfxDuration);
        }

        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.RecibirDañoJugador(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}