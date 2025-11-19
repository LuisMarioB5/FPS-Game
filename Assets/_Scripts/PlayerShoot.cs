using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject bulletPrefab; // Tu prefab de la bala
    [SerializeField] private Transform gunTip;        // El objeto vacío en la punta del cañón
    [SerializeField] private Transform cameraTransform; // Tu cámara principal

    void Update()
    {
        if (GameManager.instance.PuedeDisparar()) 
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Shoot(); 
            }
        }
    }

    void Shoot()
    {
        // 1. Encontrar el punto exacto a donde apunta el jugador (el centro de la pantalla)
        RaycastHit hit;
        Vector3 targetPoint;

        // Lanzamos un rayo invisible desde la cámara solo para saber A DÓNDE mirar
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 100f))
        {
            targetPoint = hit.point; // Si hay pared/diana, apuntamos ahí
        }
        else
        {
            // Si apuntamos al cielo, inventamos un punto muy lejano
            targetPoint = cameraTransform.position + (cameraTransform.forward * 100f);
        }

        // 2. Calcular la dirección desde el CAÑÓN hasta ese PUNTO
        Vector3 direction = targetPoint - gunTip.position;

        // 3. Instanciar la bala en la punta del cañón
        GameObject currentBullet = Instantiate(bulletPrefab, gunTip.position, Quaternion.identity);

        // 4. Rotar la bala para que mire hacia el objetivo
        currentBullet.transform.forward = direction.normalized;
    }
}