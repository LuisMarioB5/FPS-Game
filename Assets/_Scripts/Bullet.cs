using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public float speed = 40f;    // Velocidad de la bala
    public float lifeTime = 3f;  // Tiempo máximo de vida (si no choca)

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
        if (other.CompareTag("Player")) return; 

        TargetZone zone = other.GetComponent<TargetZone>();
        
        zone?.HandleHit(); // Si es diana, sumamos puntos

        Destroy(gameObject);
    }
}